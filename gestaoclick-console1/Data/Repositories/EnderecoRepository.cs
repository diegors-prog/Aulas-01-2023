using System.Linq.Expressions;
using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DataContext _context;

        public EnderecoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Endereco>> SearchAll(Expression<Func<Endereco, bool>> predicate)
        {
            return await _context.DbSetEndereco.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IList<Endereco>> GetAllAsync()
        {
            return await _context.DbSetEndereco.ToListAsync();
        }

        public async Task<Endereco> GetByIdAsync(int entityId)
        {
            return await _context.DbSetEndereco
                .FirstOrDefaultAsync(x => x.Id == entityId);
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(int fornecedorId)
        {
            return await _context.DbSetEndereco.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == fornecedorId);
        }

        public void Save(Endereco entity)
        {
            _context.DbSetEndereco.Add(entity);
        }

        public void Update(Endereco entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public bool Delete(int entityId)
        {
            var endereco = _context.DbSetEndereco.FirstOrDefault(x => x.Id == entityId);

            if (endereco == null)
                return false;
            else
            {
                _context.DbSetEndereco.Remove(endereco);
                return true;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}