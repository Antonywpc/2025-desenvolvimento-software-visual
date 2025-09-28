using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
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
app.MapGet("/api/produtos/listar", () =>
{
    //Validar a lista de produtos para saber
    //se existe algo dentro
    if (listaDeProdutos.Any())
    {
        return Results.Ok(listaDeProdutos);
    }
    return Results.NotFound("Lista Vazia!");
});

// GET: /api/produto/buscar/nome_do_produto

app.MapGet("/api/produto/buscar/{nome}", (string nome) =>
{
    // foreach (Produto produtoCadastrado in listaDeProdutos)
    // {
    //     if (produtoCadastrado.Nome == nome)
    //     {
    //         //Produto localizado
    //         return Results.Ok(produtoCadastrado);
    //         
    //     }
    // }
    //Expressão Lambda
    Produto resultado = listaDeProdutos.FirstOrDefault(p => p.Nome == nome);
    if (resultado == null)
    {
    return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(resultado);
});
    

// POST: /api/produtos/cadastrar

app.MapPost("/api/produtos/cadastrar", ([FromBody]Produto produto) =>
{
    //Não permitir o cadastro de um produto com o mesmo nome
    foreach (Produto produtoCadastrado in listaDeProdutos)
    {
        if (produtoCadastrado.Nome == produto.Nome)
        {
            //Não posso cadastrar
            return Results.Conflict("Produto já cadastrado");
        }
    }
    listaDeProdutos.Add(produto);
    return Results.Created("api/produtos/cadastrar", produto);
});




produto.Nome = "Nome teste 002";
Console.WriteLine(produto.Id);

app.Run();
