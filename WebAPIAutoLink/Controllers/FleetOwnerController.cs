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

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(FleetOwner))]
        [ProducesResponseType(400)]
        public IActionResult GetFleetOwner(int Id)
        {
            if (!_fleetOwnerRepository.FleetOwnerExists(Id))
                return NotFound();

            var fleetOwner = _mapper.Map<FleetOwnerDto>(_fleetOwnerRepository.GetFleetOwner(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fleetOwner);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFleetOwner(int Id, [FromBody] FleetOwnerDto updatedFleetOwner)
        {
            if (updatedFleetOwner == null)
                return BadRequest(ModelState);

            if (Id != updatedFleetOwner.Id)
                return BadRequest(ModelState);

            if (!_fleetOwnerRepository.FleetOwnerExists(Id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ownerMap = _mapper.Map<FleetOwner>(updatedFleetOwner);

            if (!_fleetOwnerRepository.UpdateFleetOwner(ownerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFleetOwner(int Id)
        {
            if (!_fleetOwnerRepository.FleetOwnerExists(Id))
            {
                return NotFound();
            }

            var fleetOwnerToDelete = _fleetOwnerRepository.GetFleetOwner(Id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_fleetOwnerRepository.DeleteFleetOwner(fleetOwnerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
