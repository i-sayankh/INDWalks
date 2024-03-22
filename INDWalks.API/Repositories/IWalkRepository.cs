using INDWalks.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1);
        Task<Walks> CreateAsync(Walks walks);
        Task<Walks?> GetByIdAsync(Guid id);
        Task<Walks?> UpdateAsync(Guid id, Walks walks);
        Task<Walks?> DeleteAsync(Guid id);
    }
}
