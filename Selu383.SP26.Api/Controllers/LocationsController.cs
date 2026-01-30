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
        public ActionResult<LocationDto> CreateLocation([FromBody] LocationDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid payload");

            if (dto.TableCount < 1)
                return BadRequest("TableCount must be at least 1.");

            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 120)
                return BadRequest("Name must be between 1 and 120 characters.");

            var newLocation = new Location
            {
                Name = dto.Name,
                Address = dto.Address,
                TableCount = dto.TableCount
            };

            _context.Locations.Add(newLocation);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLocationById), new { id = newLocation.Id }, new LocationDto
            {
                Id = newLocation.Id,
                Name = newLocation.Name,
                Address = newLocation.Address,
                TableCount = newLocation.TableCount
            });
        }


        [HttpPut("{id:int}")]
        public ActionResult UpdateLocation(int id, [FromBody] UpdateLocationDto dto)
        {
 
            if (dto == null)
                return BadRequest("Invalid payload.");

            if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Length > 120)
                return BadRequest("Name must be between 1 and 120 characters.");

            if (dto.TableCount < 1)
                return BadRequest("TableCount must be at least 1.");

            var location = _context.Locations.FirstOrDefault(x => x.Id == id);
            if (location == null)
                return NotFound("Location not found.");

            location.Name = dto.Name;
            location.Address = dto.Address;
            location.TableCount = dto.TableCount;

            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public ActionResult DeleteLocation(int id)
        {
            var location = _context.Locations.Find(id);

            if (location == null)
            {
                return NotFound("Location ID invalid.");
            }
            _context.Remove(location);
            _context.SaveChanges();

            return Ok($"Location ID {location.Id} successfully deleted.");
        }
    }
}