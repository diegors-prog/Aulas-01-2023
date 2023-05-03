using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.ViewModels;

namespace WebApi.Configuration
{
    public class AutoMapperDTOs : Profile
    {
        public AutoMapperDTOs()
        {
            CreateMap<Fornecedor, FornecedorDTO>().PreserveReferences().MaxDepth(0);
            CreateMap<Produto, ProdutoDTO>().PreserveReferences().MaxDepth(0);
            CreateMap<Endereco, EnderecoDTO>().PreserveReferences().MaxDepth(0);
        }
    }

    public class AutoMapperViewModels : Profile
    {
        public AutoMapperViewModels()
        {
            CreateMap<FornecedorViewModel, Fornecedor>();
            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<EnderecoViewModel, Endereco>();
        }
    }
}