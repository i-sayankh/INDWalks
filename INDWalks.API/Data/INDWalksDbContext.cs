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
    }
}
