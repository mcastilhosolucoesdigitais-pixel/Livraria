using Livraria.TJRJ.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.TJRJ.API.Infra.Mappings;

public class AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autores");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(40)
            .HasColumnType("varchar(40)");

        // Relacionamento N:N com Livro
        builder.HasMany(a => a.Livros)
            .WithMany(l => l.Autores)
            .UsingEntity<Dictionary<string, object>>(
                "LivroAutor",
                j => j.HasOne<Livro>().WithMany().HasForeignKey("LivroId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Autor>().WithMany().HasForeignKey("AutorId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("Livro_Autor");
                    j.HasKey("LivroId", "AutorId");
                });
    }
}
