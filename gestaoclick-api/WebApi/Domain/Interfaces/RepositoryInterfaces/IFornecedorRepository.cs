using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFornecedorRepository : IBaseRepository<Fornecedor>
    {
        Task<Fornecedor> GetByDocumentAsync(string entityDocument);
        Task<Fornecedor> ObterFornecedorEndereco(int fornecedorId);
        Task<Fornecedor> ObterFornecedorProdutoEndereco(int fornecedorId);
    }
}