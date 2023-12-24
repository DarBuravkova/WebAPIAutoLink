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
    public class UserController : ControllerBase
    {
        //    private readonly IUserRepository _userRepository;
        //    private readonly IMapper _mapper;

        //    public UserController(IUserRepository userRepository,
        //        IMapper mapper)
        //    {
        //        _userRepository = userRepository;
        //        _mapper = mapper;
        //    }

        //    [HttpGet]
        //    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        //    public IActionResult GetUsers()
        //    {
        //        var Users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        return Ok(Users);
        //    }

        //    [HttpGet("{Id}")]
        //    [ProducesResponseType(200, Type = typeof(User))]
        //    [ProducesResponseType(400)]
        //    public IActionResult GetUser(int Id)
        //    {
        //        if (!_userRepository.UserExists(Id))
        //            return NotFound();

        //        var User = _mapper.Map<UserDto>(_userRepository.GetUser(Id));

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        return Ok(User);
        //    }

        //    [HttpPost]
        //    [ProducesResponseType(204)]
        //    [ProducesResponseType(400)]
        //    public IActionResult CreatePokemon([FromQuery] int authId, [FromBody] UserDto userCreate)
        //    {
        //        if (userCreate == null)
        //            return BadRequest(ModelState);

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        var userMap = _mapper.Map<User>(userCreate);

        //        userMap.Authorizations = _carStatusRepository.GetCarStatus(statusId);

        //        if (!_carRepository.CreateCar(carMap))
        //        {
        //            ModelState.AddModelError("", "Something went wrong while savin");
        //            return StatusCode(500, ModelState);
        //        }

        //        return Ok("Successfully created");
        //    }

        //    [HttpPut("{Id}")]
        //    [ProducesResponseType(400)]
        //    [ProducesResponseType(204)]
        //    [ProducesResponseType(404)]
        //    public IActionResult UpdateUser([FromBody] UserDto updatedUser)
        //    {
        //        if (updatedUser == null)
        //            return BadRequest(ModelState);

        //        if (!ModelState.IsValid)
        //            return BadRequest();

        //        var UserMap = _mapper.Map<User>(updatedUser);

        //        if (!_userRepository.UpdateUser(UserMap))
        //        {
        //            ModelState.AddModelError("", "Something went wrong updating owner");
        //            return StatusCode(500, ModelState);
        //        }

        //        return NoContent();
        //    }

        //    [HttpDelete("{Id}")]
        //    [ProducesResponseType(400)]
        //    [ProducesResponseType(204)]
        //    [ProducesResponseType(404)]
        //    public IActionResult DeleteUser(int userId)
        //    {
        //        if (!_userRepository.UserExists(userId))
        //        {
        //            return NotFound();
        //        }

        //        var UserToDelete = _userRepository.GetUser(userId);

        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        if (!_userRepository.DeleteUser(UserToDelete))
        //        {
        //            ModelState.AddModelError("", "Something went wrong deleting owner");
        //        }

        //        return NoContent();
        //    }
    }
}
