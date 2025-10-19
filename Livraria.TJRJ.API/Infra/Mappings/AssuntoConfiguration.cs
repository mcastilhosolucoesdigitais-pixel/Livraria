using Livraria.TJRJ.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.TJRJ.API.Infra.Mappings;

public class AssuntoConfiguration : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder.ToTable("Assuntos");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Descricao)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        // Relacionamento N:N com Livro
        builder.HasMany(a => a.Livros)
            .WithMany(l => l.Assuntos)
            .UsingEntity<Dictionary<string, object>>(
                "LivroAssunto",
                j => j.HasOne<Livro>().WithMany().HasForeignKey("LivroId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Assunto>().WithMany().HasForeignKey("AssuntoId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("Livro_Assunto");
                    j.HasKey("LivroId", "AssuntoId");
                });
    }
}
