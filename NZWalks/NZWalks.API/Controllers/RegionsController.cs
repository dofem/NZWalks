using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;
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
    }
}
