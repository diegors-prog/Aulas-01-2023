using Domain.Entities;
using Domain.Entities.Validations;
using Domain.Interfaces;

namespace Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoService(IProdutoRepository produtoRepository, IUnitOfWork unitOfWork)
        {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

            _produtoRepository.Save(produto);
            await _unitOfWork.Commit();
            return true;
        }

        public async Task<bool> Remove(int id)
        {
            var wasRemoved = _produtoRepository.Delete(id);

            if (wasRemoved)
            {
                await _unitOfWork.Commit();
                return true;
            }
            else
                return false;
        }

        public async Task<bool> Update(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return false;

            _produtoRepository.Update(produto);
            await _unitOfWork.Commit();
            return true;
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}