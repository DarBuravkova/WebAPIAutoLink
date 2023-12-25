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
    public class FleetOwnerController : ControllerBase
    {
        private readonly IFleetOwnerRepository _fleetOwnerRepository;
        private readonly IMapper _mapper;

        public FleetOwnerController(IFleetOwnerRepository fleetOwnerRepository,
            IMapper mapper)
        {
            _fleetOwnerRepository = fleetOwnerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FleetOwner>))]
        public IActionResult GetFleetOwners()
        {
            var fleetOwners = _mapper.Map<List<FleetOwnerDto>>(_fleetOwnerRepository.GetFleetOwners());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fleetOwners);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(FleetOwner))]
        [ProducesResponseType(400)]
        public IActionResult GetFleetOwner(int id)
        {
            if (!_fleetOwnerRepository.FleetOwnerExists(id))
                return NotFound();

            var fleetOwner = _mapper.Map<FleetOwnerDto>(_fleetOwnerRepository.GetFleetOwner(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fleetOwner);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFleetOwner(int id, [FromBody] FleetOwnerDto updatedFleetOwner)
        {
            if (updatedFleetOwner == null)
                return BadRequest(ModelState);

            if (id != updatedFleetOwner.Id)
                return BadRequest(ModelState);

            if (!_fleetOwnerRepository.FleetOwnerExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var existingFleetOwner = _fleetOwnerRepository.GetFleetOwner(id);

            if (existingFleetOwner == null)
                return NotFound();

            // Update only specific properties
            existingFleetOwner.CompanyName = updatedFleetOwner.CompanyName ?? existingFleetOwner.CompanyName;
            existingFleetOwner.ContactPerson = updatedFleetOwner.ContactPerson ?? existingFleetOwner.ContactPerson;
            existingFleetOwner.Phone = updatedFleetOwner.Phone ?? existingFleetOwner.Phone;

            if (!_fleetOwnerRepository.UpdateFleetOwner(existingFleetOwner))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return Ok("Changed");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFleetOwner(int id)
        {
            if (!_fleetOwnerRepository.FleetOwnerExists(id))
            {
                return NotFound();
            }

            var fleetOwnerToDelete = _fleetOwnerRepository.GetFleetOwner(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_fleetOwnerRepository.DeleteFleetOwner(fleetOwnerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return Ok("Deleted");
        }
    }
}
