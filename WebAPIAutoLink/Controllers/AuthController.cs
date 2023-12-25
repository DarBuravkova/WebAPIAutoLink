using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebAPIAutoLink.Data;
using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly AppSettings _appSettings;

        public AuthController(DataContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        [HttpPost("Login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginDto model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
            {
                return BadRequest("Invalid email or password");
            }

            var match = CheckPassword(model.Password, user);

            if (!match)
            {
                return BadRequest("Invalid email or password");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), 
                    new Claim("email", user.Email), new Claim(ClaimTypes.Role, user.Role) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            var encrypterToken = tokenHandler.WriteToken(token);

            return Ok(new { token = encrypterToken, username = user.Email });
        }

        private bool CheckPassword(string password, User user)
        {
            bool result;

            using (HMACSHA512 hmac = new HMACSHA512(user.PasswordSalt))
            {
                var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                result = compute.SequenceEqual(user.PasswordHash);  
            }
            return result;
        }

        [HttpPost("Register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email is already registered");
                }

                var user = new User { Email = model.Email, Role = "User"};

                if (model.ConfirmPassword == model.Password)
                {
                    using (HMACSHA512 hmac = new HMACSHA512())
                    {
                        user.PasswordSalt = hmac.Key;
                        user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                    }
                }
                else
                {
                    return BadRequest("Passwords do not match");
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("Invalid registration data");
            }
        }

        [HttpPost("RegisterFleetOwner")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult RegisterFleetOwner([FromBody] RegisterFleetOwnerDto model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.FleetOwners.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email is already registered");
                }

                var user = new FleetOwner { Email = model.Email};

                if (model.ConfirmPassword == model.Password)
                {
                    using (HMACSHA512 hmac = new HMACSHA512())
                    {
                        user.PasswordSalt = hmac.Key;
                        user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                    }
                }
                else
                {
                    return BadRequest("Passwords do not match");
                }

                _context.FleetOwners.Add(user);
                _context.SaveChanges();

                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("Invalid registration data");
            }
        }

        [HttpPost("RegisterManager")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult RegisterManager([FromBody] RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email is already registered");
                }

                var user = new User { Email = model.Email, Role = "Manager" };

                if (model.ConfirmPassword == model.Password)
                {
                    using (HMACSHA512 hmac = new HMACSHA512())
                    {
                        user.PasswordSalt = hmac.Key;
                        user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
                    }
                }
                else
                {
                    return BadRequest("Passwords do not match");
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("Invalid registration data");
            }
        }
    }
}
