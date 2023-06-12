using Domain.Entities;
using Domain.Entities.Validations;
using Domain.Interfaces;

namespace Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoService(IProdutoRepository produtoRepository,
                              IUnitOfWork unitOfWork,
                              INotificadorService notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            _produtoRepository.Save(produto);
            await _unitOfWork.Commit();
        }

        public async Task Remove(int id)
        {
            var wasRemoved = _produtoRepository.Delete(id);

            if (!wasRemoved)
            {
                Notificar("Id inválido, não foi possível remover o produto");
                return;
            }

            await _unitOfWork.Commit();
        }

        public async Task Update(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            _produtoRepository.Update(produto);
            await _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}