using Microsoft.EntityFrameworkCore;
using RankList.Models;
using RankList.Repositories;
namespace RankList.Repositories
{
    public class ListaPessoalRepository : IListaPessoalRepository
    {
        private readonly AppDbContext _context;

        public ListaPessoalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarAsync(ListaPessoal lista)
        {
            _context.ListasPessoais.Add(lista);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ListaPessoal>> ListarTodosAsync()
        {
            return await _context.ListasPessoais
                .Include(l => l.Estabelecimentos)
                .ToListAsync();
        }
    }
}

