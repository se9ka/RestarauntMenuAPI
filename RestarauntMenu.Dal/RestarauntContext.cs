using Microsoft.EntityFrameworkCore;

namespace RestarauntMenu.Dal;

public sealed class RestarauntContext : DbContext
{
    public DbSet<DishEntity> Dishes { get; set; } = null!;

    public RestarauntContext(DbContextOptions<RestarauntContext> options) : base(options)
    {
    }
}
