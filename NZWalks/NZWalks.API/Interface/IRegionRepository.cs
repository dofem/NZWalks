using NZWalks.API.Model.Domain;

namespace NZWalks.API.Interface
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync();
    }
}
