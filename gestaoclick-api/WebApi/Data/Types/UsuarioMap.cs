using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Types
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.Property(i => i.Id)
                .HasColumnName("id");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.UserName)
                .HasColumnName("username")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(i => i.Password)
                .HasColumnName("password")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(i => i.Email)
                .HasColumnName("email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80)
                .IsRequired();

            // Relacionamentos
            builder
                .HasMany(i => i.Roles)
                .WithMany(i => i.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "usuario_role",
                    role => role
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("role_id")
                        .HasConstraintName("FK_usuario_role_role_id")
                        .OnDelete(DeleteBehavior.Restrict),
                    user => user
                        .HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("usuario_id")
                        .HasConstraintName("FK_usuario_role_usuario_id")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}