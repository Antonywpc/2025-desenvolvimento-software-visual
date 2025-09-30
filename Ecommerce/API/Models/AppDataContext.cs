using Microsoft.EntityFrameworkCore;

namespace API.Models;

//Classe que representa o Banco de Dados da Aplicação
// 1 - Criar a herança da classe DbContext
// 2 - Informar quais classes vão representar tabelas no banco
// 3 - Configurar a String de conexão para o Sqlite
// 4 - 


public class AppDataContext : DbContext 
{
    //Atributos representam as tabelas no banco
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=EcommerceDB.db");
    }
}