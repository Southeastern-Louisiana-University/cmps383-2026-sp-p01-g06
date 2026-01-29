using Microsoft.AspNetCore.Mvc;

namespace Selu383.SP26.Api.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly DataContext _context;

        public LocationsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<LocationDto> GetLocations()
        {
            var locations = _context.Locations.ToList();

            return Ok(locations);

        }

        [HttpGet("{id:int}")]
        public ActionResult<LocationDto> GetLocationById(int id)
        {
            var location = _context.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);

        }
        [HttpPost]
        public ActionResult CreateLocation([FromBody] CreateLocationDto dto)
        {
            if (dto.TableCount < 1)
            {
                return BadRequest("TableCount must be at least 1.");
            }
            var newLocation = new Location
            {
                Name = dto.Name,
                Address = dto.Address,
                TableCount = dto.TableCount
            };
            _context.Locations.Add(newLocation);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetLocationById), new { id = newLocation.Id }, newLocation);
        }
        //[HttpDelete("{id:int}")]
        //public ActionResult DeleteLocation(int id)
        //{
        //    var location = _context.Locations.Find(id);

        //    if (location == null)
        //    {
        //        return NotFound("Location ID invalid.");
        //    }
        //    _context.Remove(location);
        //    _context.SaveChanges();

        //    return Ok($"Location ID {location.Id} successfully deleted.");
        //}
    }
}