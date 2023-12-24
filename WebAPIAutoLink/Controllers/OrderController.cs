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

        //[HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        //public IActionResult GetOrders()
        //{
        //    var Orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrders());

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(Orders);
        //}

        //[HttpGet("{Id}")]
        //[ProducesResponseType(200, Type = typeof(Order))]
        //[ProducesResponseType(400)]
        //public IActionResult GetOrder(int id)
        //{
        //    if (!_orderRepository.OrderExists(id))
        //        return NotFound();

        //    var Order = _mapper.Map<OrderDto>(_orderRepository.GetOrder(id));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(Order);
        //}

        //[HttpGet("/isConfirmed")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Car>))]
        //public IActionResult GetNotConfirmedOrder()
        //{
        //    var reviews = _mapper.Map<List<OrderDto>>(_orderRepository.GetNotConfirmedOrder());

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(reviews);
        //}

        //[HttpGet("user/{id}")]
        //[ProducesResponseType(200, Type = typeof(Order))]
        //[ProducesResponseType(400)]
        //public IActionResult GetReviewsForAPokemon(int pokeId)
        //{
        //    var reviews = _mapper.Map<List<OrderDto>>(_orderRepository.GetUserOrders(pokeId));

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(reviews);
        //}

        //[HttpPost]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public IActionResult CreateReview([FromQuery] int userId, [FromQuery] int carId, 
        //    [FromQuery] int startLocationId, [FromQuery] int endLocationId, 
        //    [FromBody] OrderDto orderCreate)
        //{
        //    if (orderCreate == null)
        //        return BadRequest(ModelState);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var orderMap = _mapper.Map<Order>(orderCreate);

        //    orderMap.User = _userRepository.GetUser(userId);
        //    orderMap.Car = _carRepository.GetCar(carId);
        //    orderMap.StartLocationId = _locationRepository.GetLocation(startLocationId);
        //    orderMap.EndLocationId = _locationRepository.GetLocation(endLocationId);


        //    if (!_orderRepository.CreateOrder(orderMap))
        //    {
        //        ModelState.AddModelError("", "Something went wrong while savin");
        //        return StatusCode(500, ModelState);
        //    }

        //    return Ok("Successfully created");
        //}

        

        
    }
}
