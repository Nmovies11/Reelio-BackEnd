using Microsoft.EntityFrameworkCore;
using BLL.Models.Movie;
using BLL.Models.User;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UserDTO> Users { get; set; }  
    public DbSet<MovieDTO> Movies { get; set; }
}