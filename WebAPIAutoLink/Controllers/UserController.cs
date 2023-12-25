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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var Users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Users);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int Id)
        {
            if (!_userRepository.UserExists(Id))
                return NotFound();

            var User = _mapper.Map<UserDto>(_userRepository.GetUser(Id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(User);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser([FromQuery] int id, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            var existingUser = _userRepository.GetUser(id);

            if (existingUser == null)
            {
                return NotFound(); // User not found
            }

            existingUser.FirstName = updatedUser.FirstName ?? existingUser.FirstName;
            existingUser.LastName = updatedUser.LastName ?? existingUser.LastName;
            existingUser.Patronymic = updatedUser.Patronymic ?? existingUser.Patronymic;
            existingUser.BirthDate = updatedUser.BirthDate != default ? updatedUser.BirthDate : existingUser.BirthDate;
            existingUser.Phone = updatedUser.Phone ?? existingUser.Phone;

            if (!_userRepository.UpdateUser(existingUser))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var UserToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(UserToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
