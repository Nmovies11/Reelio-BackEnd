using Microsoft.EntityFrameworkCore;
using BLL.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UserDTO> Users { get; set; }   
}