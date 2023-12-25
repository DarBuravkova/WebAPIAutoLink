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
    public class CarStatusController : ControllerBase
    {
        private readonly ICarStatusRepository _carStatusRepository;
        private readonly IMapper _mapper;

        public CarStatusController(ICarStatusRepository carstatusRepository,
            IMapper mapper)
        {
            _carStatusRepository = carstatusRepository;
            _mapper = mapper;
        }

        [HttpGet("/car")]
        [ProducesResponseType(200, Type = typeof(CarStatus))]
        [ProducesResponseType(400)]
        public IActionResult GetCarStatus(int carId)
        {
            var status = _mapper.Map<CarStatusDto>(
                _carStatusRepository.GetCarStatus(carId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(status);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCarStatus(int id,
            [FromBody] CarStatusDto updatedCarStatus)
        {
            if (updatedCarStatus == null)
                return BadRequest(ModelState);

            if (id != updatedCarStatus.Id)
                return BadRequest(ModelState);

            if (!_carStatusRepository.CarStatusExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var carStatusMap = _mapper.Map<CarStatus>(updatedCarStatus);

            if (!_carStatusRepository.UpdateCarStatus(carStatusMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
