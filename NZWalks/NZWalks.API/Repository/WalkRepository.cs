using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private DataContext _context;

        public WalkRepository(DataContext context)
        {
            _context = context; 
        }

        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }


        public async Task <Walk> DeleteWalkAsync(Guid walkId)
        {
            var walk = await _context.Walks.Where(w => w.Id == walkId).FirstOrDefaultAsync();
            if (walk == null)
                return null;
            _context.Walks.Remove(walk);
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalkAsync()
        {
            return await _context.Walks.Include(x=>x.Region).Include(x=>x.walkDifficulty).ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid WalkId)
        {
            var walk = await _context.Walks.Include(x=>x.Region).Include(x=>x.walkDifficulty).Where(w => w.Id == WalkId).FirstOrDefaultAsync();
            return walk;
        }

        public async Task <Walk> UpdateWalkAsync(Guid walkId, Walk walk)
        {   
            var updatewalk = await _context.Walks.Where(w=>w.Id == walkId).FirstOrDefaultAsync();   
            if (updatewalk == null)
                return null;

            updatewalk.Name = walk.Name;
            updatewalk.Length = walk.Length;
            updatewalk.WalkDifficultyId = walk.WalkDifficultyId;    
            updatewalk.RegionId = walk.RegionId;

            await _context.SaveChangesAsync();
            return updatewalk;
             
        }

        
    }
}
