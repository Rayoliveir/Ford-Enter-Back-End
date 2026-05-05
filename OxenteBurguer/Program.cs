using Microsoft.EntityFrameworkCore;
using OxenteBurguer.Data;
using OxenteBurguer.Services;
using OxenteBurguer.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar o Banco de Dados (In-Memory para teste rápido)
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("OxenteBurguerDB"));

// 2. Registrar nossos serviços (Injeção de Dependência)
builder.Services.AddScoped<PedidoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Popular o cardápio automaticamente ao iniciar (Seu antigo cardápio do Console)
using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Produtos.Any()) {
        context.Produtos.AddRange(
            new Lanche { Id = 1, Nome = "X-Bacon", Preco = 25.00m, Categoria = "Lanche", Descricao = "Bacon crocante" },
            new Produto { Id = 7, Nome = "Refrigerante", Preco = 6.00m, Categoria = "Bebida" }
            // Adicione os outros aqui...
        );
        context.SaveChanges();
    }
}

// 4. Configurar Swagger
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();