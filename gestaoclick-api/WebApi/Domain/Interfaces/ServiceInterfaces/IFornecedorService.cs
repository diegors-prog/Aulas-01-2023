using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task<bool> Add(Fornecedor fornecedor);
        Task<bool> Update(Fornecedor fornecedor);
        Task<bool> Remove(int id);

        Task<bool> UpdateEndereco(Endereco endereco);
    }
}