// See https://aka.ms/new-console-template for more information
using Data.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<Data.Context.DataContext>();

services.AddScoped<IFornecedorRepository, FornecedorRepository>();
services.AddScoped<IProdutoRepository, ProdutoRepository>();
services.AddScoped<IEnderecoRepository, EnderecoRepository>();

services.AddScoped<IUnitOfWork, UnitOfWork>();

// Injeção de dependencia


var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var fornecedorRepository = scope.ServiceProvider.GetService<IFornecedorRepository>();
    var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
    var novoFornecedor = new Fornecedor { 
                                            Nome = "Maria Oliveira",
                                            Documento = "05285692000185",
                                            TipoFornecedor = TipoFornecedorEnum.PessoaJuridica,
                                            Ativo = true,
                                            Endereco = new Endereco { Logradouro = "Travessa da Punhalada", Numero = "650", Complemento = "Onde o Vento Faz Curva", Cep = "95560000", Bairro = "Do Quebracoco", Cidade = "Torres", Estado = "Rio Grande do Sul"}
                                        };
    fornecedorRepository.Save(novoFornecedor);
    await unitOfWork.Commit();

    var fornecedores = await fornecedorRepository.GetAllAsync();
    foreach (var fornecedor in fornecedores)
    {
        Console.WriteLine($"Nome: {fornecedor.Nome}, Email: {fornecedor.Documento}");
    }
}
