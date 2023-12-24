using WebAPIAutoLink.DTO;
using WebAPIAutoLink.Models;
using AutoLinkWebAPI.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace WebAPIAutoLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        //public static Authorization auth = new Authorization();
        //private readonly IConfiguration _configuration;
        //private readonly IUserService _userService;

        //public AuthorizationController(IConfiguration configuration, IUserService userService)
        //{
        //    _configuration = configuration;
        //    _userService = userService;
        //}

        //[HttpGet, Authorize]
        //public ActionResult<string> GetMe()
        //{
        //    var userName = _userService.GetMyName();
        //    return Ok(userName);
        //}

        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(AuthorizationDto request)
        //{
        //    CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //    auth.Login = request.Login;
        //    auth.PasswordHash = passwordHash;
        //    auth.PasswordSalt = passwordSalt;

        //    return Ok(auth);
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(AuthorizationDto request)
        //{
        //    if (auth.Login != request.Login)
        //    {
        //        return BadRequest("User not found.");
        //    }

        //    if (!VerifyPasswordHash(request.Password, auth.PasswordHash, auth.PasswordSalt))
        //    {
        //        return BadRequest("Wrong password.");
        //    }

        //    string token = CreateToken(auth);

        //    var refreshToken = GenerateRefreshToken();
        //    SetRefreshToken(refreshToken);

        //    return Ok(token);
        //}

        //[HttpPost("refresh-token")]
        //public async Task<ActionResult<string>> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];

        //    if (!auth.RefreshToken.Equals(refreshToken))
        //    {
        //        return Unauthorized("Invalid Refresh Token.");
        //    }
        //    else if (auth.TokenExpires < DateTime.Now)
        //    {
        //        return Unauthorized("Token expired.");
        //    }

        //    string token = CreateToken(auth);
        //    var newRefreshToken = GenerateRefreshToken();
        //    SetRefreshToken(newRefreshToken);

        //    return Ok(token);
        //}

        //private RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        Expires = DateTime.Now.AddDays(7),
        //        Created = DateTime.Now
        //    };

        //    return refreshToken;
        //}

        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

        //    auth.RefreshToken = newRefreshToken.Token;
        //    auth.TokenCreated = newRefreshToken.Created;
        //    auth.TokenExpires = newRefreshToken.Expires;
        //}

        //private string CreateToken(Authorization auth)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, auth.Login),
        //        new Claim(ClaimTypes.Role, "Admin")
        //    };

        //    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: creds);

        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //    return jwt;
        //}

        //private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        return computedHash.SequenceEqual(passwordHash);
        //    }
        //}
    }
}
