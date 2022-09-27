using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.AddRequest;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.UpdateRequest;
using System.Diagnostics.Contracts;

namespace NZWalks.API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalkController(IWalkRepository walkRepository,IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkAsync()
        {
            var walks = await _walkRepository.GetAllWalkAsync();
            var walkDTO = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walkDTO);
        }

        [HttpGet("{WalkId:Guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid WalkId)
        {   //get wwalk domain from database
            var walk = await _walkRepository.GetWalkAsync(WalkId);
            if (walk == null)
                return NotFound();
            // Convert domain object to DtO
            var walkDto = _mapper.Map<WalkDto>(walk);
            //return the value
            return Ok(walkDto);
        }

        [HttpDelete("{walkId:Guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid walkId)
        {
            var walk = await _walkRepository.DeleteWalkAsync(walkId);
            if (walk == null)
                return null;
            var walkDTO = _mapper.Map<WalkDto>(walk);
            return Ok(walkDTO);
        }   

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {   //Convert DtO to domain object
            var walkDomain = new Walk()
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };
            //pass domain object to repository to persist this
            walkDomain = await _walkRepository.AddWalkAsync(walkDomain);

            //convert domain object back to Dto
            var walkdTo = new Walk()
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };

            //send DtO response back to client
            return CreatedAtAction(nameof(GetWalkAsync), new { walkId = walkdTo.Id }, walkdTo);

        }

        [HttpPut ("{walkId:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid walkId, [FromBody] UpdateWalk updateWalk)
        {       //convert dto to domain object
            var walkdomain = new Walk()
            {
                Length = updateWalk.Length,
                Name = updateWalk.Name,
                RegionId = updateWalk.RegionId,
                WalkDifficultyId = updateWalk.WalkDifficultyId,

            };
            //Pass details to repository
             walkdomain = await _walkRepository.UpdateWalkAsync(walkId,walkdomain);
            //Handle null(not found)
            if (walkdomain == null)
                return null;
            //Convert back to dto
            var walkdTo = new Walk()
            {
                Id = walkdomain.Id,
                Length = walkdomain.Length,
                Name = walkdomain.Name,
                RegionId = walkdomain.RegionId,
                WalkDifficultyId = walkdomain.WalkDifficultyId,

            };
                //Return response
            return Ok(walkdTo);

        }

        

    }
}
