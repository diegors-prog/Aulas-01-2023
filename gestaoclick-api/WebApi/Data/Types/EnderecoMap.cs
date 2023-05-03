using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("enderecos");

            builder.Property(i => i.Id).HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.FornecedorId)
                .HasColumnName("fornecedor_id")
                .IsRequired();

            builder.Property(i => i.Logradouro)
                .HasColumnName("logradouro")
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(i => i.Numero)
                .HasColumnName("numero")
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(i => i.Cep)
                .HasColumnName("cep")
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(i => i.Complemento)
                .HasColumnName("complemento")
                .HasColumnType("varchar(250)");

            builder.Property(i => i.Bairro)
                .HasColumnName("bairro")
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(i => i.Cidade)
                .HasColumnName("cidade")
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(i => i.Estado)
                .HasColumnName("estado")
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}