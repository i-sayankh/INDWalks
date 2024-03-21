using INDWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Data
{
    public class INDWalksDbContext : DbContext
    {
        public INDWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walks> Walks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Difficulties
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("7d63a568-0db8-4dc8-ad25-3e9b2418cf57"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("976d24b9-46c5-4460-aa09-ba748114159e"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("a39137e0-e4ca-430b-b729-4c47e12454e5"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
