using Microsoft.EntityFrameworkCore;
using OxenteBurguer.Data;
using OxenteBurguer.Services;
using OxenteBurguer.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Serviços
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=oxenteburguer.db"));
builder.Services.AddScoped<PedidoService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. BUILD (Criação da variável 'app')
var app = builder.Build();

// 3. Uso da variável 'app' (Populando o banco)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!context.Produtos.Any())
    {
        context.Produtos.AddRange(
                new Lanche { Id = 1, Nome = "X-Bacon", Preco = 25.00m, Categoria = "Lanche", Descricao = "Hambúrguer artesanal, bacon crocante, queijo e molho especial" },
                new Lanche { Id = 2, Nome = "X-Salada", Preco = 22.00m, Categoria = "Lanche", Descricao = "Hambúrguer suculento, queijo, alface, tomate e maionese da casa" },
                new Lanche { Id = 3, Nome = "X-Frango", Preco = 23.50m, Categoria = "Lanche", Descricao = "Filé de frango grelhado, queijo, alface e molho especial" },
                new Lanche { Id = 4, Nome = "X-Tudo", Preco = 30.00m, Categoria = "Lanche", Descricao = "Hambúrguer, bacon, ovo, presunto, queijo, alface e tomate" },
                new Lanche { Id = 5, Nome = "Cheeseburger", Preco = 20.00m, Categoria = "Lanche", Descricao = "Hambúrguer clássico com queijo derretido e pão macio" },
                new Lanche { Id = 6, Nome = "Misto Quente", Preco = 12.50m, Categoria = "Lanche", Descricao = "Pão Artesanal, Queijo, Presunto" },
                new Produto { Id = 7, Nome = "Refrigerante Lata", Preco = 6.00m, Categoria = "Bebida" },
                new Produto { Id = 8, Nome = "Suco de Laranja", Preco = 7.00m, Categoria = "Bebida" },
                new Produto { Id = 9, Nome = "Suco de Abacaxi", Preco = 7.00m, Categoria = "Bebida" },
                new Produto { Id = 10, Nome = "Suco de Maracujá", Preco = 7.00m, Categoria = "Bebida" },
                new Produto { Id = 11, Nome = "Água Mineral", Preco = 4.00m, Categoria = "Bebida" },
                new Produto { Id = 12, Nome = "Água com Gás", Preco = 4.50m, Categoria = "Bebida" },
                new Produto { Id = 13, Nome = "Suco de Acerola", Preco = 8.00m, Categoria = "Bebida" }
        );
        context.SaveChanges();
    }
}

// 4. Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();