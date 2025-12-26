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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("6f1c2a9e-3b4d-4f8a-9c2e-1a5b7d8e0f12"), 
                Name = "Easy"
            },
            new Difficulty()
            {
                Id = Guid.Parse("8a3d5c1e-7f4b-4e2a-8d9c-0f1a2b3c4d5e"), 
                Name = "Medium"
            },
            new Difficulty()
            {
                Id = Guid.Parse("9b2e4f7a-1c5d-4a8e-b0c3-6d7e8f1a2b34"), 
                Name = "Hard"
            }
        };
        // Seed difficulties to the database
        modelBuilder.Entity<Difficulty>().HasData(difficulties);

        var regions = new List<Region>()
        {
            new Region()
            {
                Id = Guid.Parse("a1c3e5f7-2b4d-4a6e-9c8f-0d1e2b3a4c5d"),
                Name = "North",
                Code = "NTH",
                RegionImageUrl = null
            },
            new Region()
            {
                Id = Guid.Parse("b2d4f6a8-1c3e-4b5d-9f0e-7a8c1e2d3f4b"),
                Name = "South",
                Code = "STH",
                RegionImageUrl = null
            },
            new Region()
            {
                Id = Guid.Parse("d6b6a0e7-8b9c-9d0e-3f1a-6b7c8d9e0123"),
                Name = "East",
                Code = "EST",
                RegionImageUrl = null
            },
            new Region()
            {                
                Id = Guid.Parse("c3e5a7b9-2d4f-4c6e-8a1b-0f2d3e4c5a6b"),
                Name = "West",
                Code = "WST",
                RegionImageUrl = null
            }
        };
        modelBuilder.Entity<Region>().HasData(regions);
    }
}