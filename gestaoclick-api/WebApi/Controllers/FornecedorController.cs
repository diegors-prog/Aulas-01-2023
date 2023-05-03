using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/fornecedores")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository, IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var fornecedores = _mapper.Map<IList<FornecedorDTO>>(await _fornecedorRepository.GetAllAsync());
            return HttpMessageOk(fornecedores);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var fornecedor = _mapper.Map<FornecedorDTO>(await _fornecedorRepository.GetByIdAsync(id));
            return HttpMessageOk(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(FornecedorViewModel model)
        {
            if (!ModelState.IsValid) return HttpMessageError("Dados incorretos");

            var fornecedor = _mapper.Map<Fornecedor>(model);
            var result = await _fornecedorService.Add(fornecedor);

            if (!result) return HttpMessageError("Dados invalidos");

            return HttpMessageOk(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, FornecedorViewModel model)
        {
            if (!ModelState.IsValid) return HttpMessageError("Dados incorretos");

            var fornecedor = _mapper.Map<Fornecedor>(model);
            var result = await _fornecedorService.Update(fornecedor);

            if (!result) return HttpMessageError("Dados invalidos");

            return HttpMessageOk(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var produto = await _fornecedorRepository.ObterFornecedorEndereco(id);

            if (produto == null) return NotFound();

            var result = await _fornecedorService.Remove(id);

            if (!result) return HttpMessageError("Não foi possível remover o fornecedor");

            return HttpMessageOk(id);
        }

        [HttpGet("obter-fornecedor-produto-endereco/{id:int}")]
        public async Task<IActionResult> ObterProdutosFornecedores(int id)
        {
            var fornecedor = _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorProdutoEndereco(id));
            return HttpMessageOk(fornecedor);
        }

        private IActionResult HttpMessageOk(dynamic data = null)
        {
            if (data == null)
                return NoContent();
            else
                return Ok(new
                {
                    data,
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