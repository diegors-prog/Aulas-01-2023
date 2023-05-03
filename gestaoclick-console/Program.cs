// See https://aka.ms/new-console-template for more information
using Data.Context;
using Data.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataContext>(
            x => x.UseSqlite(builder.Configuration.GetConnectionString("DataSource=app.db;Cache=Shared"))
        );

        builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


        var app = builder.Build();
        var serviceProvider = builder.Services.BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var fornecedorRepository = scope.ServiceProvider.GetService<IFornecedorRepository>();
            var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
            var novoFornecedor = new Fornecedor { Nome = "João da Silva", Documento = "02074112041", TipoFornecedor = TipoFornecedorEnum.PessoaFisica, Ativo = true, Endereco = new Endereco { Logradouro = "Beco do Laçasso", Numero = "345", Complemento = "Na frente da maloca", Cep = "95560000", Bairro = "Vila do Diabão", Cidade = "Torres", Estado = "Rio Grande do Sul"} };
            fornecedorRepository.Save(novoFornecedor);
            await unitOfWork.Commit();

            var fornecedores = await fornecedorRepository.GetAllAsync();
            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"Nome: {fornecedor.Nome}, Email: {fornecedor.Documento}");
            }
        }
