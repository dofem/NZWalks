using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private DataContext _context;

        public RegionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await _context.regions.ToListAsync();
        }
    }
}
