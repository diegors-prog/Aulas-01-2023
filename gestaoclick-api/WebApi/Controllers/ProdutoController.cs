using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/produtos")]
    public class ProdutoController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(INotificadorService notificador,
                                 IProdutoRepository produtoRepository,
                                 IProdutoService produtoService,
                                 IMapper mapper,
                                 IUser user) : base(notificador, user)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CustomResponse(_mapper.Map<IList<ProdutoDTO>>(await _produtoRepository.GetAllAsync()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var produto = _mapper.Map<ProdutoDTO>(await _produtoRepository.GetByIdAsync(id));

            if (produto == null) return NotFound();

            return CustomResponse(produto);
        }

        [ClaimsAuthorize("Produto","Adicionar")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(ProdutoViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var produto = _mapper.Map<Produto>(model);

            produto.Imagem = UploadImage();
            produto.DataCadastro = DateTime.UtcNow;

            await _produtoService.Add(produto);

            return CustomResponse(_mapper.Map<ProdutoDTO>(produto));
        }

        [ClaimsAuthorize("Produto","Atualizar")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ProdutoViewModel model)
        {
            if (id != model.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(model);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var produto = _mapper.Map<Produto>(model);
            produto.Imagem = UploadImage();

            await _produtoService.Update(produto);

            return CustomResponse(_mapper.Map<ProdutoDTO>(produto));
        }

        [ClaimsAuthorize("Produto","Remover")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null) return NotFound();

            await _produtoService.Remove(id);

            return CustomResponse(_mapper.Map<ProdutoDTO>(produto));
        }

        [HttpGet("obter-produtos-fornecedores")]
        public async Task<IActionResult> ObterProdutosFornecedores()
        {
            return CustomResponse(_mapper.Map<IList<ProdutoDTO>>(await _produtoRepository.ObterProdutosFornecedores()));
        }

        private string UploadImage()
        {
            var typeImage = ".png";

            return Guid.NewGuid().ToString() + typeImage;
        }
    }
}