using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly INDWalksDbContext context;

        public RegionController(INDWalksDbContext context)
        {
            this.context = context;
        }

        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/region
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data from DB - Domain Models
            var regions = context.Regions.ToList();

            // Map Domain Models to DTOs
            var regionsDTO = new List<RegionDTO>();
            foreach(var region in regions)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code= region.Code,
                    Name= region.Name,
                    RegionImageUrl= region.RegionImageUrl
                });
            }

            // Return DTOs
            return Ok(regionsDTO);
        }

        // GET REGION BY ID
        // GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            // Get Region Domain Model from DB
            var region = context.Regions.Find(id);

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
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            // Map DTO to Domain Model
            var region = new Region
            {
                Code= addRegionRequestDTO.Code,
                Name= addRegionRequestDTO.Name,
                RegionImageUrl= addRegionRequestDTO.RegionImageUrl
            };

            // Use Domain Model to Create Region
            context.Regions.Add(region);
            context.SaveChanges();

            // map domain model back to DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl= region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDTO.Id}, regionDTO);
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            // Check if Region Exists
            var region = context.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            region.Code = updateRegionRequestDTO.Code;
            region.Name = updateRegionRequestDTO.Name;
            region.RegionImageUrl= updateRegionRequestDTO.RegionImageUrl;

            context.SaveChanges();

            // Convert Domain Model to DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl= region.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/region/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var region = context.Regions.FirstOrDefault(x => x.Id == id);

            if(region== null)
            {
                return NotFound();
            }

            // Delete Region
            context.Regions.Remove(region);
            context.SaveChanges();

            return Ok();
        }
    }
}
