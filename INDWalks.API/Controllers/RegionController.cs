using AutoMapper;
using INDWalks.API.CustomActionFilters;
using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTOs;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionController(INDWalksDbContext context, IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/region
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from DB - Domain Models
            var regions = await regionRepository.GetAllAsync();

            // Return DTOs
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // Get Region Domain Model from DB
            var region = await regionRepository.GetByIdAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            // Map Domain Model to Region DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Return DTO
            return Ok(regionDTO);
        }

        // POST To Create new Region
        // POST: https://localhost:portnumber/api/region

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            var region = mapper.Map<Region>(addRegionRequestDTO);

            // Use Domain Model to Create Region
            await regionRepository.CreateAsync(region);

            // map domain model back to DTO
            var regionDTO = mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Map DTO to Domain Model
            var region = mapper.Map<Region>(updateRegionRequestDTO);

            // Check if Region exists
            region = await regionRepository.UpdateAsync(id, region);
            if (region == null) return NotFound();

            // Convert Domain Model to DTO
            var regionDTO = mapper.Map<RegionDTO>(region);

            return Ok(regionDTO);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/region/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async  Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if(region== null)
            {
                return NotFound();
            }

            // Return deleted Region back
            // map Domain model to DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDTO);
        }
    }
}
