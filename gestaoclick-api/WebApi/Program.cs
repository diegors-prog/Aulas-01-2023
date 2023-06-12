using Data.Context;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Notificacoes;
using WebApi.Configuration;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options => {
        options.SuppressModelStateInvalidFilter = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentityConfig(builder.Configuration);

builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<INotificadorService, NotificadorService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUser, AspNetUser>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
