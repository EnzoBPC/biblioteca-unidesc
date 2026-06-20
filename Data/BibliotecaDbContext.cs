using Microsoft.EntityFrameworkCore;
using biblioteca.Models;

namespace biblioteca.Data;

public class BibliotecaDbContext : DbContext
{
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options)
    {
    }

    public DbSet<Livro> Livros { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
}