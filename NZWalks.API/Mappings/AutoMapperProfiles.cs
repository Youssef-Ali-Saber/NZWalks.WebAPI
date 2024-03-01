using AutoMapper;
using NZWalks.WebAPI.Models.Domain;
using NZWalks.WebAPI.Models.DTO;

namespace NZWalks.WebAPI.Mappings
{
	public class AutoMapperProfiles:Profile
	{
		
        public AutoMapperProfiles()
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<Region,AddRegionRequestDTO>().ReverseMap();
            CreateMap<Region,UpdateRegionRequestDTO>().ReverseMap();
            CreateMap<Walk,AddWalkRequestDTO>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();
            CreateMap<Walk,UpdateWalkRequestDTO>().ReverseMap();
            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
        }
    }
}
