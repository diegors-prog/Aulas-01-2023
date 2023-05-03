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

        public FornecedorService(IFornecedorRepository fornecedorRepository, IEnderecoRepository enderecoRepository, IUnitOfWork unitOfWork)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Add(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
                || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return false;

            if (_fornecedorRepository.SearchAll(x => x.Documento == fornecedor.Documento).Result.Any())
                return false;

            _fornecedorRepository.Save(fornecedor);
            await _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> Remove(int id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutoEndereco(id).Result.Produtos.Any()) return false;

            var endereco = await _enderecoRepository.ObterEnderecoPorFornecedor(id);

            if (endereco != null)
                _enderecoRepository.Delete(endereco.Id);
                
            _fornecedorRepository.Delete(id);
            await _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> Update(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return false;

            if (_fornecedorRepository.SearchAll(x => x.Documento == fornecedor.Documento && x.Id != fornecedor.Id).Result.Any())
                return false;

            _fornecedorRepository.Update(fornecedor);
            await _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> UpdateEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return false;

            _enderecoRepository.Update(endereco);
            await _unitOfWork.Commit();
            return true;
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}