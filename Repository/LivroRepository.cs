using biblioteca.Models;
using biblioteca.Data;

namespace biblioteca.Repository;

public class LivroRepository : ILivrosRepository
{
    private readonly BibliotecaDbContext _context;

    public LivroRepository(BibliotecaDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Livro> ObterTodos()
    {
        return _context.Livros.ToList();
    }

    public Livro ObterPorId(int id)
    {
        return _context.Livros.Find(id);
    }

    public void Adicionar(Livro livro)
    {
        _context.Livros.Add(livro);
        _context.SaveChanges();
    }

    public void Atualizar(Livro livro)
    {
        _context.Livros.Update(livro);
        _context.SaveChanges();
    }

    public void Excluir(int id)
    {
        var livro = _context.Livros.Find(id);
        if (livro != null)
        {
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }
    }
}