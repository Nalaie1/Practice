using Microsoft.EntityFrameworkCore;
using NamPractice.API.Models.Domain;

namespace NamPractice.API.Data;

public class PracticeDbContext : DbContext
{
    public PracticeDbContext(DbContextOptions contextOptions) : base(contextOptions)
    {
        
    }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Models.Domain.Practice> Practices { get; set; }
}