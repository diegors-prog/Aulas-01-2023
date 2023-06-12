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
    [Route("api/fornecedores")]
    public class FornecedorController : MainController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(INotificadorService notificador,
                                    IFornecedorRepository fornecedorRepository,
                                    IFornecedorService fornecedorService,
                                    IMapper mapper,
                                    IUser user) : base(notificador, user)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return CustomResponse(_mapper.Map<IList<FornecedorDTO>>(await _fornecedorRepository.GetAllAsync()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var fornecedor = _mapper.Map<FornecedorDTO>(await _fornecedorRepository.GetByIdAsync(id));

            if (fornecedor == null) return NotFound();

            return CustomResponse(fornecedor);
        }

        [ClaimsAuthorize("Fornecedor","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> AddAsync(FornecedorViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(model);
            await _fornecedorService.Add(fornecedor);

            return CustomResponse(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        [ClaimsAuthorize("Fornecedor","Atualizar")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FornecedorDTO>> UpdateAsync(int id, FornecedorViewModel model)
        {
            if (id != model.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(model);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(model);
            await _fornecedorService.Update(fornecedor);

            return CustomResponse(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        [ClaimsAuthorize("Fornecedor","Remover")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var fornecedor = await _fornecedorRepository.GetByIdAsync(id);

            if (fornecedor == null) return NotFound();

            await _fornecedorService.Remove(id);

            return CustomResponse(_mapper.Map<FornecedorDTO>(fornecedor));
        }

        [HttpGet("obter-fornecedor-produto-endereco/{id:int}")]
        public async Task<IActionResult> ObterProdutosFornecedores(int id)
        {
            var fornecedor = _mapper.Map<FornecedorDTO>(await _fornecedorRepository.ObterFornecedorProdutoEndereco(id));

            if (fornecedor == null) return NotFound();

            return CustomResponse(fornecedor);
        }
    }
}