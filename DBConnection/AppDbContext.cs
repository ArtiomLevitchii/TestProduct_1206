using Microsoft.EntityFrameworkCore;
using TestProduct.Models;

namespace TestProduct.DBConnection;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<ActionLogModel> Logs { get; set; }
}
