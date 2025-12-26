using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NamPractice.API.Data;
using NamPractice.API.Models.Domain;
using Practice.API.Models.DTO;
using Practice.API.Repositories;

namespace Practice.API.Controllers
{
    // https://localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly PracticeDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(PracticeDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
        }

        // GET all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task <IActionResult> GetAllRegions()
        {
            //Get data from database - Regions table
            var regionsDomain = await regionRepository.GetAllAsync();
            
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
        public async Task <IActionResult> GetById([FromRoute]Guid id)
        {
            var regionDomain = await regionRepository.GetIdAsync(id);
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
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomain = new Region()
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            
            //Pass Domain Model to DBContext to save to database
            await regionRepository.CreateAsync(regionDomain);
            
            //Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            
            //Return DTO back to client
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Get region from database
            var regionDomain = await regionRepository.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            
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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region()
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };
            
            //Get region from database
            var  regionDomain = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomain == null)
            {
                return NotFound();
            }
            
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
