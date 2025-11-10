using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
builder.Services.AddCors(
    options => options.AddPolicy("Acesso Total",
    configs => configs.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader())
    );
var app = builder.Build();


//Funcionalidade - Requisições
// - URL/Caminho/Endereço
// - Um método HTTP

app.MapGet("/", () => "API de produtos");

Produto produto = new Produto();

// Criando a lista de produtos
List<Produto> listaDeProdutos = new List<Produto>();

// Adicionando os 10 produtos à lista usando inicializadores de objeto
// listaDeProdutos.Add(new Produto { Nome = "Notebook Gamer Nitro 5", Quantidade = 15, Preco = 7500.00 });
// listaDeProdutos.Add(new Produto { Nome = "Smartphone Galaxy S25", Quantidade = 30, Preco = 5999.90 });
// listaDeProdutos.Add(new Produto { Nome = "Monitor LED 27\" Ultrawide", Quantidade = 22, Preco = 1850.50 });
// listaDeProdutos.Add(new Produto { Nome = "Teclado Mecânico RGB HyperX", Quantidade = 50, Preco = 450.00 });
// listaDeProdutos.Add(new Produto { Nome = "Mouse sem Fio Logitech MX Master 4", Quantidade = 75, Preco = 599.99 });
// listaDeProdutos.Add(new Produto { Nome = "Cadeira Gamer DXRacer", Quantidade = 12, Preco = 2300.00 });
// listaDeProdutos.Add(new Produto { Nome = "SSD NVMe 1TB Kingston", Quantidade = 40, Preco = 899.90 });
// listaDeProdutos.Add(new Produto { Nome = "Headset Gamer Astro A50", Quantidade = 18, Preco = 1500.00 });
// listaDeProdutos.Add(new Produto { Nome = "Placa de Vídeo RTX 5070", Quantidade = 8, Preco = 6200.00 });
// listaDeProdutos.Add(new Produto { Nome = "Memória RAM DDR5 16GB Corsair", Quantidade = 60, Preco = 750.00 });


// GET: /api/produtos/listar
app.MapGet("/api/produtos/listar", ([FromServices] AppDataContext dbContext) =>
{
    //Validar a lista de produtos para saber
    //se existe algo dentro
    if (dbContext.Produtos.Any())
    {
        return Results.Ok(dbContext.Produtos.ToList());
    }
    return Results.NotFound("Lista Vazia!");
});

// GET: /api/produtos/buscar/nome_do_produto

app.MapGet("/api/produtos/buscar/{nome}", ([FromRoute] string nome, [FromServices] AppDataContext dbContext) =>
{
    var produto = dbContext.Produtos
        .FirstOrDefault(p => p.Nome.ToLower() == nome.ToLower());

    if (produto == null)
        return Results.NotFound("Produto não encontrado!");

    return Results.Ok(produto);
});

    

// POST: /api/produtos/cadastrar

app.MapPost("/api/produtos/cadastrar", ([FromBody]Produto produto, [FromServices] AppDataContext dbContext) =>
{
    //Não permitir o cadastro de um produto com o mesmo nome

    Produto? resultado = dbContext.Produtos.FirstOrDefault(p => p.Nome == produto.Nome);
    if (resultado == null)
    {
        dbContext.Produtos.Add(produto);
        dbContext.SaveChanges();
        return Results.Created("api/produtos/cadastrar", produto);
    }
    return Results.Conflict("Produto já cadastrado!");
    
});


// DELETE: /api/produtos/deletar/id

app.MapPatch("/api/produtos/alterar/{id:int}", ([FromRoute] int id, [FromBody] Produto produtoAtualizado, [FromServices] AppDataContext dbContext) =>
{
    var produto = dbContext.Produtos.Find(id);
    if (produto == null)
        return Results.NotFound("Produto não encontrado!");

    produto.Nome = produtoAtualizado.Nome;
    produto.Quantidade = produtoAtualizado.Quantidade;
    produto.Preco = produtoAtualizado.Preco;

    dbContext.SaveChanges();

    return Results.Ok($"Produto '{produto.Nome}' atualizado com sucesso!");
});




// UPDATE: /api/produtos/update/id

app.MapPatch("/api/produtos/alterar/{id:int}", 
    ([FromRoute] int id, [FromBody] Produto produtoAtualizado, [FromServices] AppDataContext dbContext) =>
    {
        var produto = dbContext.Produtos.Find(id); // usa a PK diretamente
        if (produto == null) return Results.NotFound("Produto não encontrado!");

        produto.Nome = produtoAtualizado.Nome;
        produto.Quantidade = produtoAtualizado.Quantidade;
        produto.Preco = produtoAtualizado.Preco;

        dbContext.SaveChanges();
        return Results.Ok(produto);
    });


produto.Nome = "Nome teste 002";
Console.WriteLine(produto.Id);

app.UseCors("Acesso Total");
app.Run();
