using Microsoft.EntityFrameworkCore;
using PostgresWebApi.Models;

namespace PostgresWebApi.Data
{
    public class MarketDbContext(DbContextOptions<MarketDbContext> options) : DbContext(options)
    {
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Marca> Marcas => Set<Marca>();
    }
}
