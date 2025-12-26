using AutoMapper;
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
        private readonly IMapper mapper;

        public RegionsController(PracticeDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task <IActionResult> GetAllRegions()
        {
            //Get data from database - Regions table
            var regionsDomain = await regionRepository.GetAllAsync();
            
            //Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
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
            // Return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomain = mapper.Map<Region>(addRegionRequestDto);
            
            //Pass Domain Model to DBContext to save to database
            regionDomain =  await regionRepository.CreateAsync(regionDomain);
            
            //Map Domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            
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
            
            //Return deleted region back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            
            //Get region from database
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            
            //Return updated region back to client
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
