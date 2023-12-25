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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IUserRepository userRepository,
            ICarRepository carRepository, ILocationRepository locationRepository,
                IMapper mapper)
        {
            _orderRepository = orderRepository;
            _carRepository = carRepository;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public IActionResult GetOrders()
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrders());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int id)
        {
            var order = _orderRepository.GetOrder(id);

            if (order == null)
                return NotFound();

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpGet("/isConfirmed")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        public IActionResult GetNotConfirmedOrder()
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetNotConfirmedOrder());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersForAUser(int id)
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetUserOrders(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromQuery] int userId, [FromQuery] int carId,
            [FromQuery] int endLocationId, [FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Order>(orderCreate);

            var car = _carRepository.GetCar(carId);
            if (car == null)
            {
                ModelState.AddModelError("", "Car not found");
                return BadRequest(ModelState);
            }

            if (car.IsRented)
            {
                ModelState.AddModelError("", "Car is already rented");
                return BadRequest(ModelState);
            }

            decimal price = orderCreate.UsageTime * car.Price;
            orderMap.Price = price;

            orderMap.User = _userRepository.GetUser(userId);
            orderMap.Car = car; 
            orderMap.StartLocationId = car.Locations.Id;
            orderMap.EndLocationId = endLocationId;

            car.IsRented = true;

            if (!_orderRepository.CreateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("ConfirmOrder/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ConfirmOrder(int id)
        {
            var order = _orderRepository.GetOrder(id);

            if (order == null)
            {
                return NotFound(); 
            }

            if (order.IsConfirmed)
            {
                ModelState.AddModelError("", "Order is already confirmed");
                return BadRequest(ModelState);
            }

            order.IsConfirmed = true;

            if (!_orderRepository.UpdateOrder(order))
            {
                ModelState.AddModelError("", "Something went wrong while updating the order");
                return StatusCode(500, ModelState);
            }

            return Ok("Order confirmed successfully");
        }

    }
}
