using AutoMapper;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTOs;

namespace INDWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<Region, AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDTO>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walks>().ReverseMap();
            CreateMap<Walks, WalkDTO>().ReverseMap();
            CreateMap<Walks, UpdateWalkRequestDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        }
    }
}
