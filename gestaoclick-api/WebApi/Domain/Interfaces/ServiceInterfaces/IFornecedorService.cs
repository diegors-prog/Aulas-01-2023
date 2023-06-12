using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        Task Add(Fornecedor fornecedor);
        Task Update(Fornecedor fornecedor);
        Task Remove(int id);

        Task UpdateEndereco(Endereco endereco);
    }
}