using Microsoft.EntityFrameworkCore;
using OxenteBurguer.Models;

namespace OxenteBurguer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Isso ajuda o banco a entender que Lanche é um tipo de Produto
            modelBuilder.Entity<Lanche>().HasBaseType<Produto>();
            base.OnModelCreating(modelBuilder);
        }
    }
}