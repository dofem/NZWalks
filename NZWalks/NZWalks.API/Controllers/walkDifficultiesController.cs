using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.AddRequest;
using NZWalks.API.Interface;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository;
using NZWalks.API.UpdateRequest;
using NZWalks.API.UpdateRequest;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class walkDifficultiesController : Controller
    {
        private IWalkDifficultiesRepository _walkDifficultiesRepository;
        private IMapper _mapper;

        public walkDifficultiesController( IWalkDifficultiesRepository walkDifficultiesRepository,IMapper mapper)
        {
            _walkDifficultiesRepository = walkDifficultiesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
           var walkdifficulties = await  _walkDifficultiesRepository.GetAllWalkDifficulties();
            var walkDTO = _mapper.Map<WalkDiffyDto>(walkdifficulties);
            return Ok(walkDTO); 

        }

        [HttpGet("{Id:Guid}")]
        [ActionName("GetDifficultiesById")]
        public async Task <IActionResult> GetDifficultiesById(Guid Id)
        {
            var walkdifficulty = await _walkDifficultiesRepository.GetDifficultiesById(Id);
            if (walkdifficulty == null)
                return NotFound();
            var walkDTO = _mapper.Map<WalkDiffyDto>(walkdifficulty);
            return Ok(walkDTO);
        }

        [HttpDelete("{Id:Guid}")]
        public async Task<IActionResult> DeleteDifficulties(Guid Id)
        {
            var walkdifficulty = await _walkDifficultiesRepository.DeleteDifficulties(Id);
            if (walkdifficulty == null)
                return NotFound();
            var walkDTO = _mapper.Map<WalkDiffyDto>(walkdifficulty);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task <IActionResult> AddDifficulties([FromBody] AddWalkDifficulties addWalkDifficulties)
        {   if(!ValidateAddDifficulties(addWalkDifficulties))
            {
                return BadRequest("something went wrong");
            }

            var diffydomain = new WalkDifficulty()
            {
                Code = addWalkDifficulties.Code,
            };

            diffydomain = await _walkDifficultiesRepository.AddDifficulties(diffydomain);

            var diffydTo = new WalkDifficulty()
            {
                Id   = diffydomain.Id,
                Code = diffydomain.Code,
            };
            return CreatedAtAction(nameof(GetDifficultiesById), new { Id = diffydTo.Id }, diffydTo);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDifficulties([FromRoute] Guid Id, [FromBody] UpdateDifficulty updateDifficulty)
        {   if (!ValidateUpdateDifficulties(updateDifficulty))
            {
                return BadRequest("something went wrong");
            }

            //convert DTO to domain model
            var walkdiffy = new WalkDifficulty()
            {

                Code = updateDifficulty.Code,
                
            };
            //update region using repository
            walkdiffy = await _walkDifficultiesRepository.UpdateDifficulties(Id, walkdiffy);
            //if null then Not Found
            if (walkdiffy == null)
                return null;
            //convert domain back to DTO
            var WalkDTO = new WalkDiffyDto()
            {
                Id = walkdiffy.Id,
                Code = walkdiffy.Code,
                
            };

            //Return Ok response
            return Ok(WalkDTO);
        }

        //Validate AddRequest
        private bool ValidateAddDifficulties(AddWalkDifficulties addWalkDifficulties)
        {
            if (addWalkDifficulties == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficulties), $"This doesnt exist");
                return false;
            }
            
            if (string.IsNullOrEmpty(addWalkDifficulties.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficulties.Code), $"This must not be null or empty");
            }
            if(ModelState.ErrorCount >0)
            {
                return false;
            }
            return true;

        }

        private bool ValidateUpdateDifficulties(UpdateDifficulty updateDifficulty)
        {
            if (updateDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateDifficulty), $"This doesnt exist");
                return false;
            }

            if (string.IsNullOrEmpty(updateDifficulty.Code))
            {
                ModelState.AddModelError(nameof(updateDifficulty.Code), $"This must not be null or empty");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;

        }
    }
}
