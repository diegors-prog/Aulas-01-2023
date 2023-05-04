using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEnderecoRepository : IBaseRepository<Endereco>
    {
        Task<Endereco> ObterEnderecoPorFornecedor(int fornecedorId);
    }
}