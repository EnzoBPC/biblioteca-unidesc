using biblioteca.Models;

namespace biblioteca.Repository;

public interface ILivrosRepository
{
    IEnumerable<Livro> ObterTodos();
    Livro ObterPorId(int id);
    void Adicionar(Livro livro);
    void Atualizar(Livro livro);
    void Excluir(int id);
}