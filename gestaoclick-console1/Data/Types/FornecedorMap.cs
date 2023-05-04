using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("fornecedores");

            builder.Property(i => i.Id).HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(i => i.Documento)
                .HasColumnName("documento")
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(i => i.TipoFornecedor)
                .HasColumnName("tipo_fornecedor")
                .IsRequired();

            builder.Property(i => i.Ativo)
                .HasColumnName("ativo")
                .IsRequired();

            // 1 : 1 => Fornecedor : Endereco
            builder.HasOne(i => i.Endereco)
                .WithOne(e => e.Fornecedor);

            // 1 : N => Fornecedor : Produtos
            builder.HasMany(i => i.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);
        }
    }
}