using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Interface
{
    public interface IWalkDifficultiesRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties();
        Task<WalkDifficulty> GetDifficultiesById(Guid Id);
        Task<WalkDifficulty> AddDifficulties(WalkDifficulty walkDifficulties);
        Task<WalkDifficulty> UpdateDifficulties(Guid Id, WalkDifficulty walkDifficulties);

        Task<WalkDifficulty> DeleteDifficulties(Guid Id);
    }
}
