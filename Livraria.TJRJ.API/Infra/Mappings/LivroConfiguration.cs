using Livraria.TJRJ.API.Domain.Entities;
using Livraria.TJRJ.API.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livraria.TJRJ.API.Infra.Mappings;

public class LivroConfiguration : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable("Livros");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.Property(l => l.Titulo)
            .IsRequired()
            .HasMaxLength(40)
            .HasColumnType("varchar(40)");

        builder.Property(l => l.Editora)
            .IsRequired()
            .HasMaxLength(40)
            .HasColumnType("varchar(40)");

        builder.Property(l => l.Edicao)
            .IsRequired();

        builder.Property(l => l.AnoPublicacao)
            .IsRequired()
            .HasMaxLength(4)
            .HasColumnType("varchar(4)");

        // Configuração dos preços como Owned Entity (Value Object)
        builder.OwnsMany(l => l.Precos, precos =>
        {
            precos.ToTable("Livro_Precos");

            precos.WithOwner().HasForeignKey("LivroId");

            precos.Property<int>("Id")
                .ValueGeneratedOnAdd();

            precos.HasKey("Id");

            precos.Property(p => p.Valor)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            precos.Property(p => p.FormaDeCompra)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (FormaDeCompra)Enum.Parse(typeof(FormaDeCompra), v))
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            // Índice para garantir que não haja preços duplicados para a mesma forma de compra
            precos.HasIndex("LivroId", nameof(FormaDeCompra))
                .IsUnique();
        });
    }
}
