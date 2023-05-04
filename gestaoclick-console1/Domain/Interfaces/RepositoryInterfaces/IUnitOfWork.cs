namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();

        IFornecedorRepository FornecedorRepository {get;}
        IProdutoRepository ProdutoRepository {get;}
        IEnderecoRepository EnderecoRepository {get;}
    }
}