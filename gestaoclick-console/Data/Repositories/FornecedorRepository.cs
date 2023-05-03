using System.Linq.Expressions;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly DataContext _context;

        public FornecedorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Fornecedor>> SearchAll(Expression<Func<Fornecedor, bool>> predicate)
        {
            return await _context.DbSetFornecedor.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IList<Fornecedor>> GetAllAsync()
        {
            return await _context.DbSetFornecedor.ToListAsync();
        }

        public async Task<Fornecedor> GetByIdAsync(int entityId)
        {
            return await _context.DbSetFornecedor
                .FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<Fornecedor> GetByDocumentAsync(string entityDocument)
        {
            return await _context.DbSetFornecedor
                .FirstOrDefaultAsync(x => x.Documento == entityDocument);
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(int fornecedorId)
        {
            return await _context.DbSetFornecedor.AsNoTracking()
                .Include(j => j.Endereco)
                .FirstOrDefaultAsync(x => x.Id == fornecedorId);
        }

        public async Task<Fornecedor> ObterFornecedorProdutoEndereco(int fornecedorId)
        {
            return await _context.DbSetFornecedor.AsNoTracking()
                .Include(j => j.Endereco)
                .Include(j => j.Produtos)
                .FirstOrDefaultAsync(x => x.Id == fornecedorId);
        }

        public void Save(Fornecedor entity)
        {
            _context.DbSetFornecedor.Add(entity);
        }

        public void Update(Fornecedor entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public bool Delete(int entityId)
        {
            var fornecedor = _context.DbSetFornecedor.FirstOrDefault(x => x.Id == entityId);

            if (fornecedor == null)
                return false;
            else
            {
                _context.DbSetFornecedor.Remove(fornecedor);
                return true;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}