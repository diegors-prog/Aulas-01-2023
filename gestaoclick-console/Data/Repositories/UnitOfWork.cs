using Data.Context;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        private IFornecedorRepository _FornecedorRepository;

        private IProdutoRepository _ProdutoRepository;

        private IEnderecoRepository _EnderecoRepository;

        public IFornecedorRepository FornecedorRepository
        {
            get { return _FornecedorRepository ??= new FornecedorRepository(_context); }
        }

        public IProdutoRepository ProdutoRepository
        {
            get { return _ProdutoRepository ??= new ProdutoRepository(_context); }
        }

        public IEnderecoRepository EnderecoRepository
        {
            get { return _EnderecoRepository ??= new EnderecoRepository(_context); }
        }
    }
}