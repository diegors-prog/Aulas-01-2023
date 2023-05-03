using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IProdutoService produtoService, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var produtos = _mapper.Map<IList<ProdutoDTO>>(await _produtoRepository.GetAllAsync());
            return HttpMessageOk(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var produto = _mapper.Map<ProdutoDTO>(await _produtoRepository.GetByIdAsync(id));
            return HttpMessageOk(produto);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProdutoViewModel model)
        {
            if (!ModelState.IsValid) return HttpMessageError("Dados incorretos");

            var produto = _mapper.Map<Produto>(model);
            produto.Imagem = UploadImage();
            produto.DataCadastro = DateTime.UtcNow;
            var result = await _produtoService.Add(produto);

            if (!result) return HttpMessageError("Dados invalidos");

            return HttpMessageOk(_mapper.Map<ProdutoDTO>(produto));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, ProdutoViewModel model)
        {
            if (!ModelState.IsValid) return HttpMessageError("Dados incorretos");

            var produto = _mapper.Map<Produto>(model);
            produto.Imagem = UploadImage();
            var result = await _produtoService.Update(produto);

            if (!result) return HttpMessageError("Dados invalidos");

            return HttpMessageOk(_mapper.Map<ProdutoDTO>(produto));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);

            if (produto == null) return NotFound();

            var result = await _produtoService.Remove(id);

            if (!result) return HttpMessageError("Não foi possível remover o produto");

            return HttpMessageOk(id);
        }

        [HttpGet("obter-produtos-fornecedores")]
        public async Task<IActionResult> ObterProdutosFornecedores()
        {
            var produtos = _mapper.Map<IList<ProdutoDTO>>(await _produtoRepository.ObterProdutosFornecedores());
            return HttpMessageOk(produtos);
        }

        private string UploadImage()
        {
            var typeImage = ".png";

            return Guid.NewGuid().ToString() + typeImage;
        }

        private IActionResult HttpMessageOk(dynamic data = null)
        {
            if (data == null)
                return NoContent();
            else
                return Ok(new
                {
                    data
                });
        }

        private IActionResult HttpMessageError(string message = "")
        {
            return BadRequest(new
            {
                message
            });
        }
    }
}