using ApiBloodyTree.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiBloodyTree.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Membro> Membros { get; set; }
    }
}
