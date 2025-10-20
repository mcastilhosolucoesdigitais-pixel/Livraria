using Livraria.TJRJ.API.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Livraria.TJRJ.API.Middleware;

/// <summary>
/// Middleware global para tratamento de erros
/// Implementa o padrão RFC 7807 (ProblemDetails)
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Ocorreu uma exceção não tratada: {Message}", exception.Message);

        var problemDetails = CreateProblemDetails(context, exception);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(problemDetails, options);
        await context.Response.WriteAsync(json);
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        return exception switch
        {
            ValidationException validationEx => new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Erro de Validação",
                Detail = validationEx.Message,
                Instance = context.Request.Path,
                Extensions =
                {
                    ["errors"] = validationEx.Errors
                }
            },

            LivroNaoEncontradoException or
            AutorNaoEncontradoException or
            AssuntoNaoEncontradoException => new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "Recurso Não Encontrado",
                Detail = exception.Message,
                Instance = context.Request.Path
            },

            DomainException or
            ArgumentException => new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Erro de Domínio",
                Detail = exception.Message,
                Instance = context.Request.Path
            },

            FluentValidation.ValidationException fluentValidationEx => new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Erro de Validação",
                Detail = "Ocorreram um ou mais erros de validação.",
                Instance = context.Request.Path,
                Extensions =
                {
                    ["errors"] = fluentValidationEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        )
                }
            },

            _ => new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Erro Interno do Servidor",
                Detail = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                Instance = context.Request.Path
            }
        };
    }
}
