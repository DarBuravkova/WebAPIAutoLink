﻿using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Interfaces;
using WebAPIAutoLink.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIAutoLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;
        private readonly IFleetOwnerRepository _fleetOwnerRepository;
        private readonly ICarStatusRepository _carStatusRepository;

        public CarController(ICarRepository carRepository,
            IMapper mapper,
            ILocationRepository locationRepository,
            IFleetOwnerRepository fleetOwnerRepository,
            ICarStatusRepository carStatusRepository)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _locationRepository = locationRepository;
            _fleetOwnerRepository = fleetOwnerRepository;
            _carStatusRepository = carStatusRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCars()
        {
            var reviews = _mapper.Map<List<CarDto>>(_carRepository.GetCars());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int id)
        {
            if (!_carRepository.CarExists(id))
                return NotFound();

            var car = _mapper.Map<CarDto>(_carRepository.GetCar(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(car);
        }

        [HttpGet("/isRented")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetAvaliableCar()
        {
            var reviews = _mapper.Map<List<CarDto>>(_carRepository.GetAvailableCars());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("/Location/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarsByLocation(int id)
        {
            var reviews = _mapper.Map<List<CarDto>>(_carRepository.GetCarsByLocation(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("/FleetOwner/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarsByOwner(int id)
        {
            var reviews = _mapper.Map<List<CarDto>>(_carRepository.GetCarsByOwner(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCar([FromQuery] int ownerId, [FromQuery] int statusId, [FromQuery] int locationId, [FromBody] CarDto carCreate)
        {
            if (carCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carMap = _mapper.Map<Car>(carCreate);

            carMap.FleetOwners = _fleetOwnerRepository.GetFleetOwner(ownerId);
            carMap.Locations = _locationRepository.GetLocation(locationId);
            carMap.CarStatus = _carStatusRepository.GetCarStatus(statusId);

            if (!_carRepository.CreateCar(carMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCar(int id, [FromBody] CarDto updatedCar)
        {
            if (updatedCar == null)
                return BadRequest(ModelState);

            if (id != updatedCar.Id)
                return BadRequest(ModelState);

            if (!_carRepository.CarExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var carMap = _mapper.Map<Car>(updatedCar);

            if (!_carRepository.UpdateCar(carMap))
            {
                ModelState.AddModelError("", "Something went wrong updating review");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCar(int id)
        {
            if (!_carRepository.CarExists(id))
            {
                return NotFound();
            }

            var carToDelete = _carRepository.GetCar(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_carRepository.DeleteCar(carToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }

        // Added missing delete range of reviews by a reviewer **>CK
        [HttpDelete("/FleetOwner/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewsByReviewer(int id)
        {
            if (!_fleetOwnerRepository.FleetOwnerExists(id))
                return NotFound();

            var carsToDelete = _fleetOwnerRepository.GetCarsByOwner(id).ToList();
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_carRepository.DeleteCars(carsToDelete))
            {
                ModelState.AddModelError("", "error deleting reviews");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
