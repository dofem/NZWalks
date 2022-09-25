using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;
using System.Data.Common;

namespace NZWalks.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {

        }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> walkDifficulties { get; set; }

    }
}
