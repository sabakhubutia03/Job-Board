using Job_Board_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Board_API.Job_Board.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; } 
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Company> Companies { get; set; } 
}