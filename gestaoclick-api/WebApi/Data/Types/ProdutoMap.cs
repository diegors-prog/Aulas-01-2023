using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("produtos");

            builder.Property(i => i.Id).HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.FornecedorId)
                .HasColumnName("fornecedor_id")
                .IsRequired();

            builder.Property(i => i.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(i => i.Descricao)
                .HasColumnName("descricao")
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(i => i.Imagem)
                .HasColumnName("imagem")
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(i => i.Valor)
                .HasColumnName("valor")
                .IsRequired();

            builder.Property(i => i.DataCadastro)
                .HasColumnName("data_cadastro")
                .IsRequired();

            builder.Property(i => i.Ativo)
                .HasColumnName("ativo")
                .IsRequired();
        }
    }
}