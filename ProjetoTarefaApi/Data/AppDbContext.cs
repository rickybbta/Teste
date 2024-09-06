using Microsoft.EntityFrameworkCore;
using ProjetoTarefaApi.Models;

namespace ProjetoTarefaApi.Data // Ajuste o namespace conforme necessário
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Defina suas DbSets aqui
        public DbSet<Usuario> Usuarios { get; set; } // Substitua pelo nome do seu modelo
    }
}
