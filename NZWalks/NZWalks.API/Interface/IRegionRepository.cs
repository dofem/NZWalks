using NZWalks.API.Model.Domain;

namespace NZWalks.API.Interface
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region>GetRegionByIdAsync(Guid regionId);
        Task<Region> AddRegionAsync(Region region); 
        Task<Region> DeleteRegionAsync(Guid regionId);
        Task<Region> UpdateRegionAsync(Guid Id, Region region);
    }
}
