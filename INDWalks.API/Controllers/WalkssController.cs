using AutoMapper;
using INDWalks.API.CustomActionFilters;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTOs;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkssController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalkssController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        // Get Walks
        // GET: /api/walkss?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walks = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);
            return Ok(mapper.Map<List<WalkDTO>>(walks));
        }

        // Get Walk by Id
        // GET: /api/walkss/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if(walk == null) return NotFound();

            return Ok(mapper.Map<WalkDTO>(walk));
        }

        //Create Walk
        // POST: /api/walkss
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Map DTO to Domain Model
            var walk = mapper.Map<Walks>(addWalkRequestDTO);
            await walkRepository.CreateAsync(walk);

            // Map Domain model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walk);
            return Ok(walkDTO);
        }

        // Update Walk by Id
        // PUT: /api/walkss/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            // Map DTO to Domain Model
            var walk = mapper.Map<Walks>(updateWalkRequestDTO);

            walk = await walkRepository.UpdateAsync(id, walk);

            if (walk == null) return NotFound();

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDTO>(walk));
        }

        // Delete Walk by Id
        // DELETE: /api/walkss/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalk = await walkRepository.DeleteAsync(id);
            if (deletedWalk == null) return NotFound();

            return Ok(mapper.Map<WalkDTO>(deletedWalk));
        }
    }
}
