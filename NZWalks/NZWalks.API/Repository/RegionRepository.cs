using Microsoft.AspNetCore.Mvc;
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

        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await _context.regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegionAsync(Guid regionId)
        {
            var region = await _context.regions.Where(r => r.Id == regionId).FirstOrDefaultAsync();
            if (region == null)
                return null;
             _context.regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await _context.regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(Guid regionId)
        {
            return await _context.regions.Where(r => r.Id == regionId).FirstOrDefaultAsync();
        }

        public async Task<Region> UpdateRegionAsync(Guid Id, Region region)
        {
            var existingregion = await _context.regions.Where(r => r.Id == Id).FirstOrDefaultAsync();
            if (region == null)
                return null;
            existingregion.code = region.code;
            existingregion.Name = region.Name;
            existingregion.Area = region.Area;
            existingregion.Long = region.Long;
            existingregion.Lat  = region.Lat;
            existingregion.Population = region.Population;

            await _context.SaveChangesAsync();
            return existingregion;
        }
    }
}
