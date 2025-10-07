namespace API.Models;

public class Categoria
{
    public int Id { get; set; }
    public String Nome { get; set; } = String.Empty;
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
}