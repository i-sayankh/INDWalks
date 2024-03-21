using INDWalks.API.Models.Domain;

namespace INDWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walks>> GetAllAsync();
        Task<Walks> CreateAsync(Walks walks);
        Task<Walks?> GetByIdAsync(Guid id);
        Task<Walks?> UpdateAsync(Guid id, Walks walks);
        Task<Walks?> DeleteAsync(Guid id);
    }
}
