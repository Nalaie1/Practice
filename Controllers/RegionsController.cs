using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamPractice.API.Models.Domain;

namespace Practice.API.Controllers
{
    // https://localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        // GET all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "North America",
                    Code = "NA",
                    RegionImageUrl = "https://www.pexels.com/photo/statue-of-liberty-290386/"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "South America",
                    Code = "SA",
                    RegionImageUrl = "https://www.pexels.com/photo/africa-map-illustration-52502/"
                }
            };
            return Ok(regions);
        }
    }
}
