using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task <IActionResult> GetAllRegions()
        {
            //Get data from database - Regions table
            var regionsDomain = await dbContext.Regions.ToListAsync();
            
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
        public async Task <IActionResult> GetRegionById([FromRoute]Guid id)
        {
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomain = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            
            //Pass Domain Model to DBContext to save to database
            await dbContext.Regions.AddAsync(regionDomain);
            dbContext.SaveChangesAsync();
            
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
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //Get region from database
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            
            //Remove region
            dbContext.Regions.Remove(regionDomain);
            await dbContext.SaveChangesAsync();
            
            //Map Domain to DTO if needed
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            //Return deleted region back to client
            return Ok(regionDto);
        }
        
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Get region from database
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            
            //Update region details
            regionDomain.Name = updateRegionRequestDto.Name;
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            
            dbContext.SaveChangesAsync();
            
            //Map Domain to DTO if needed
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            //Return updated region back to client
            return Ok(regionDto);
        }
    }
}
