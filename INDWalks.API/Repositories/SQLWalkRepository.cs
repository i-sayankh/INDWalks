using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace INDWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly INDWalksDbContext context;

        public SQLWalkRepository(INDWalksDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            [FromQuery] string? sortBy = null, [FromQuery] bool isAscending = true,
            int pageNumber = 1, int pageSize = 1)
        {
            var walks = context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filter
            if(!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }                
            }

            // Sorting
            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            //return await context.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
        public async Task<Walks?> GetByIdAsync(Guid id)
        {
            return await context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Walks> CreateAsync(Walks walks)
        {
            await context.Walks.AddAsync(walks);
            await context.SaveChangesAsync();

            return walks;
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walks)
        {
            var existingWalk = await context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) return null;

            existingWalk.Name = walks.Name;
            existingWalk.Description = walks.Description;
            existingWalk.LengthInKm = walks.LengthInKm;
            existingWalk.WalkImageUrl = walks.WalkImageUrl;
            existingWalk.RegionId = walks.RegionId;
            existingWalk.DifficultyId = walks.DifficultyId;

            await context.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walks?> DeleteAsync(Guid id)
        {
            var existingWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) return null;

            context.Remove(existingWalk);
            await context.SaveChangesAsync();

            return existingWalk;
        }
    }
}
