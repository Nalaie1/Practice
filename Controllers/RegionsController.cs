using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamPractice.API.Data;
using NamPractice.API.Models.Domain;
using Practice.API.Models.DTO;

namespace Practice.API.Controllers
{
    // https://localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly PracticeDbContext dbContext;

        public RegionsController(PracticeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            //Get data from database - Regions table
            var regionsDomain = dbContext.Regions.ToList();
            
            //Map data from Domain to DTOs if needed
            var RegionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                RegionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            
            //Return DTOs
            return Ok(RegionsDto);
        }

        // GET region by id
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById([FromRoute]Guid id)
        {
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
                
            }
            
            //Map Domain to DTO if needed
            var RegionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            // Return DTO back to client
            return Ok(RegionDto);
        }

        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomain = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            
            //Pass Domain Model to DBContext to save to database
            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();
            
            //Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            //Return DTO back to client
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }
    }
}
