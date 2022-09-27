using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.AddRequest;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Update_Request;
using System.Reflection.Metadata.Ecma335;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await _regionRepository.GetAllRegionsAsync();
           var regionDTO =_mapper.Map<List<Region>>(regions);
            return Ok(regionDTO);
        }


        [HttpGet("{regionId:Guid}")]
        [ActionName("GetRegionByIdAsync")]
        public async Task<IActionResult> GetRegionByIdAsync(Guid regionId)
        {
           var region = await _regionRepository.GetRegionByIdAsync(regionId);
            if (region == null)
                return NotFound();
            var regionDTO = _mapper.Map<Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addregionrequest)
        {
            var region = new Region()
            {
                code = addregionrequest.code,
                Area = addregionrequest.Area,
                Lat = addregionrequest.Lat,
                Long = addregionrequest.Long,
                Name = addregionrequest.Name,
                Population = addregionrequest.Population
            };
            region = await _regionRepository.AddRegionAsync(region);

            var regionDTO = new Region()
            {   Id   = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionByIdAsync), new{ regionId = regionDTO.Id },regionDTO);

        }

        [HttpDelete("{regionId:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid regionId)
        {
            //Get region from the db
            var region =await _regionRepository.DeleteRegionAsync(regionId);
            //If null,return NotFound
            if (region == null)
                return NotFound();
            //convert response back to DTO
            var regionDTO = new RegionDto
            {
                Id = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            
            //return Ok response
            return Ok(regionDTO);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid Id, [FromBody] UpdateRegion updateRegion)
        {
            //convert DTO to domain model
            var region = new Region()
            {
                
                code = updateRegion.code,
                Area = updateRegion.Area,
                Lat = updateRegion.Lat,
                Long = updateRegion.Long,
                Name = updateRegion.Name,
                Population = updateRegion.Population
            };
            //update region using repository
            region =await _regionRepository.UpdateRegionAsync(Id, region);
            //if null then Not Found
            if (region == null)
                return null;
            //convert domain back to DTO
            var regionDTO = new RegionDto
            {
                Id = region.Id,
                code = region.code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            //Return Ok response
            return Ok(regionDTO);
        }
    }
}
