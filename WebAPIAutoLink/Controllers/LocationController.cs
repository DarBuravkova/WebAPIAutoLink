using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Interfaces;
using WebAPIAutoLink.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIAutoLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationController(ILocationRepository LocationRepository,
            IMapper mapper)
        {
            _locationRepository = LocationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Location>))]
        public IActionResult GetLocations()
        {
            var Locations = _mapper.Map<List<LocationDto>>(_locationRepository.GetLocations());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Locations);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Location))]
        [ProducesResponseType(400)]
        public IActionResult GetLocation(int Id)
        {
            if (!_locationRepository.LocationExists(Id))
                return NotFound();

            var location = _mapper.Map<LocationDto>(_locationRepository.GetLocation(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(location);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLocation([FromBody] LocationDto locationCreate)
        {
            if (locationCreate == null)
                return BadRequest(ModelState);

            var address = _locationRepository.GetLocations()
                .Where(a => a.Address.Trim().ToUpper() == locationCreate.Address.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (address != null)
            {
                ModelState.AddModelError("", "Location already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var locationMap = _mapper.Map<Location>(locationCreate);


            if (!_locationRepository.CreateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLocation(int Id, [FromBody] LocationDto updatedLocation)
        {
            if (updatedLocation == null)
                return BadRequest(ModelState);

            if (Id != updatedLocation.Id)
                return BadRequest(ModelState);

            if (!_locationRepository.LocationExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var locationMap = _mapper.Map<Location>(updatedLocation);

            if (!_locationRepository.UpdateLocation(locationMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLocation(int Id)
        {
            if (!_locationRepository.LocationExists(Id))
            {
                return NotFound();
            }

            var locationToDelete = _locationRepository.GetLocation(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_locationRepository.DeleteLocation(locationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
