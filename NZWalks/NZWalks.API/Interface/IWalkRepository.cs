using NZWalks.API.Model.Domain;

namespace NZWalks.API.Interface
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalkAsync();
        Task <Walk> GetWalkAsync(Guid walkId);
        Task <Walk> AddWalkAsync(Walk walk);
        Task <Walk> UpdateWalkAsync(Guid walkId, Walk walk);
        Task <Walk> DeleteWalkAsync(Guid walkId);
    }
}
