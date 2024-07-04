using ERP;
using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }

    // Define aquí los DbSet correspondientes a tus entidades
    public DbSet<Product> Products { get; set; }
    // Otros DbSet según tus modelos
}
