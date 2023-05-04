using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<IList<Produto>> ObterProdutosPorFornecedor(int fornecedorId);
        Task<IList<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(int id);
    }
}