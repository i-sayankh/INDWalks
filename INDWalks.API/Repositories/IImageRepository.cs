using INDWalks.API.Models.Domain;

namespace INDWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
