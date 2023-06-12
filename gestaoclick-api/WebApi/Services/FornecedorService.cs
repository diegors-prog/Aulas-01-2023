using Domain.Entities;
using Domain.Entities.Validations;
using Domain.Interfaces;

namespace Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoRepository,
                                 IUnitOfWork unitOfWork,
                                 INotificadorService notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Add(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

            if (_fornecedorRepository.SearchAll(x => x.Documento == fornecedor.Documento).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            _fornecedorRepository.Save(fornecedor);
            await _unitOfWork.Commit();
        }

        public async Task Remove(int id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutoEndereco(id).Result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados!");
                return;
            }

            var endereco = await _enderecoRepository.ObterEnderecoPorFornecedor(id);

            if (endereco != null)
                _enderecoRepository.Delete(endereco.Id);
                
            _fornecedorRepository.Delete(id);
            await _unitOfWork.Commit();
        }

        public async Task Update(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (_fornecedorRepository.SearchAll(x => x.Documento == fornecedor.Documento && x.Id != fornecedor.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            _fornecedorRepository.Update(fornecedor);
            await _unitOfWork.Commit();
        }

        public async Task UpdateEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            _enderecoRepository.Update(endereco);
            await _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}