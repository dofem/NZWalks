using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository
{
    public class WalkDifficultiesRepository : IWalkDifficultiesRepository
    {
        private DataContext _context;

        public WalkDifficultiesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<WalkDifficulty> AddDifficulties(WalkDifficulty walkDifficulties)
        {
            walkDifficulties.Id = new Guid();
            await _context.walkDifficulties.AddAsync(walkDifficulties);
            _context.SaveChanges();
            return walkDifficulties;       
        }

        public async Task<WalkDifficulty> DeleteDifficulties(Guid Id)
        {
            var walkdiffy = await _context.walkDifficulties.Where(w => w.Id == Id).FirstOrDefaultAsync();
            if (walkdiffy == null)
                return null;
            _context.walkDifficulties.Remove(walkdiffy);
            return walkdiffy;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
        {
           return await _context.walkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetDifficultiesById(Guid Id)
        {
            return await _context.walkDifficulties.Where(w => w.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<WalkDifficulty> UpdateDifficulties(Guid Id, WalkDifficulty walkDifficulties)
        {
            var update = await _context.walkDifficulties.Where(w=> w.Id == Id).FirstOrDefaultAsync();
            if (update == null)
                return null;

            update.Code = walkDifficulties.Code;
            _context.SaveChangesAsync();
            return update;
        }
    }
}
