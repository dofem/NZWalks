using AutoMapper;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Profiles
{
    public class Helper:Profile
    {
        public Helper()
        {
            CreateMap<Region, RegionDto>();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<WalkDifficulty, WalkDiffyDto>().ReverseMap();
        }
    }
}
