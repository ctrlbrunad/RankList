using Microsoft.EntityFrameworkCore;
using RankList.Models;

namespace RankList.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<ListaPessoal> ListasPessoais { get; set; }
        public DbSet<ListaPessoalEstabelecimento> ListaPessoalEstabelecimentos { get; set; }
        // Adicione outros DbSet conforme necessário

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
