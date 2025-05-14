using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Sector> Sectors { get; init; } = default!;
    public DbSet<Form> Forms { get; init; } = default!;
    public DbSet<SectorInForm> SectorInForms { get; init; } = default!;
}