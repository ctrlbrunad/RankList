using RankList.Models;

namespace RankList.Repositories
{
    public interface IListaPessoalRepository
    {
        Task CriarAsync(ListaPessoal lista);
        Task<List<ListaPessoal>> ListarTodosAsync();
    }
}