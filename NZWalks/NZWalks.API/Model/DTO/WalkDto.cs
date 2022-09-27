using NZWalks.API.Model.Domain;

namespace NZWalks.API.Model.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid WalkDifficultyId { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public WalkDifficulty walkDifficulty { get; set; }
    }
}
