using System.ComponentModel.DataAnnotations;

namespace biblioteca.Models;

public class Reserva
{
    public int Id { get; set; }
    
    public int LivroId { get; set; }
    
    public Livro Livro { get; set; }
    
    public string UsuarioEmail { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime DataReserva { get; set; }
    public int NumeroPessoas { get; set; } 
}