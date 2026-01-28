using Microsoft.AspNetCore.Mvc;

namespace Selu383.SP26.Api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        //Static List of Fake Seeded Data
        private static readonly List<LocationDto> Locations = new()
        {
            new LocationDto { Id = 1, Name = "test1", Address = "123 Main St", TableCount = 10 },
            new LocationDto { Id = 2, Name = "test2", Address = "987 South St", TableCount = 5 },
            new LocationDto { Id = 3, Name = "test3", Address = "456 West St", TableCount = 8 }
        };

        //Returns the static list of locations
        [HttpGet]
        public ActionResult<List<LocationDto>> GetLocations()
        {
            return Ok(Locations);
        }

        [HttpGet("{id:int}")]
        public ActionResult<LocationDto> GetLocationById(int id)
        {
            var location = Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);

        }
    }
}