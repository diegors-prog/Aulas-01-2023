using Data.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Fornecedor> DbSetFornecedor { get; set; }
        public DbSet<Produto> DbSetProduto { get; set; }
        public DbSet<Endereco> DbSetEndereco { get; set; }
        public DbSet<Usuario> DbSetUsuario { get; set; }
        public DbSet<Role> DbSetRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
        }
    }
}