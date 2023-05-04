using System.Linq.Expressions;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;

        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Produto>> SearchAll(Expression<Func<Produto, bool>> predicate)
        {
            return await _context.DbSetProduto.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IList<Produto>> GetAllAsync()
        {
            return await _context.DbSetProduto.ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int entityId)
        {
            return await _context.DbSetProduto
                .FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<Produto> ObterProdutoFornecedor(int id)
        {
            return await _context.DbSetProduto.AsNoTracking()
                .Include(j => j.Fornecedor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<Produto>> ObterProdutosFornecedores()
        {
            return await _context.DbSetProduto.AsNoTracking()
                .Include(j => j.Fornecedor)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<IList<Produto>> ObterProdutosPorFornecedor(int fornecedorId)
        {
            return await SearchAll(x => x.FornecedorId == fornecedorId);
        }

        public void Save(Produto entity)
        {
            _context.DbSetProduto.Add(entity);
        }

        public void Update(Produto entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public bool Delete(int entityId)
        {
            var produto = _context.DbSetProduto.FirstOrDefault(x => x.Id == entityId);

            if (produto == null)
                return false;
            else
            {
                _context.DbSetProduto.Remove(produto);
                return true;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}