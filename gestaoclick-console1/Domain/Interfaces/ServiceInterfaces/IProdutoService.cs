using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task<bool> Add(Produto produto);
        Task<bool> Update(Produto produto);
        Task<bool> Remove(int id);
    }
}