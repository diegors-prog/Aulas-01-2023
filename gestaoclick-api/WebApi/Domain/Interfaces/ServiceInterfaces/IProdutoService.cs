using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoService : IDisposable
    {
        Task Add(Produto produto);
        Task Update(Produto produto);
        Task Remove(int id);
    }
}