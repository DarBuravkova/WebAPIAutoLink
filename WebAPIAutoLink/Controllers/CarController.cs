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
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCars());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Car))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int id)
        {
            if (!_carRepository.CarExists(id))
            {
                ModelState.AddModelError("", "Car not found");
                return BadRequest(ModelState);
            }

            var car = _mapper.Map<CarDto>(_carRepository.GetCar(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(car);
        }

        [HttpGet("/isRented")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetAvaliableCar()
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetAvailableCars());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cars);
        }

        [HttpGet("/Location/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetCarsByLocation(int id)
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCarsByLocation(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cars);
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
        public IActionResult CreateCar([FromQuery] int ownerId, [FromQuery] int locationId, [FromBody] CarDto carCreate)
        {
            if (carCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var carMap = _mapper.Map<Car>(carCreate);

            carMap.FleetOwners = _fleetOwnerRepository.GetFleetOwner(ownerId);
            carMap.Locations = _locationRepository.GetLocation(locationId);

            var carStatus = new CarStatus
            {
                Timestamp = DateTime.UtcNow,
                OdometerReading = 0, 
                FuelLevel = 100,   
                MaintenanceStatus = "Good", 
            };

            carStatus.CarId = carMap.Id;

            carMap.CarStatus = carStatus;

            if (!_carRepository.CreateCar(carMap) || !_carStatusRepository.CreateCarStatus(carStatus))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPost]
        [Route("AddPhoto")]
        public IActionResult AddPhoto(int carId)
        {
            if (!HttpContext.Request.HasFormContentType)
            {
                return BadRequest("Unsupported media type");
            }

            var file = HttpContext.Request.Form.Files[0].OpenReadStream();
            byte[] carPhoto;

            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                carPhoto = memoryStream.ToArray();
            }

            var car = _carRepository.GetCar(carId);

            if (car == null)
            {
                return NotFound();
            }

            car.Photo = carPhoto;

            _carRepository.UpdateCar(car);

            return Ok("Car photo added successfully");
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

            return Ok("Changed");
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

            return Ok("Deleted");
        }

        [HttpDelete("/FleetOwner/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCarsByOwner(int id)
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
            return Ok("Deleted");
        }
    }
}
