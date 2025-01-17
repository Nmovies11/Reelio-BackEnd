using Microsoft.EntityFrameworkCore;
using Common.Entities;
using DAL.Entities.User;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }

}