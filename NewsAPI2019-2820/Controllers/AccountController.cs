using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsAPI2019_2820.Models;
using NewsAPI2019_2820.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI2019_2820.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenProvider tokenProvider;

        public AccountController(ApplicationDbContext context, ITokenProvider tokenProvider)
        {
            this._db = context;
            this.tokenProvider = tokenProvider;
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromForm] string username, [FromForm] string password)
        {
            var user = _db.Users
                    .Where(x => x.Username == username)
                    .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user != null)
            {
                if (user.Password != password)
                {
                    return BadRequest("Invalid credentials.");
                }
            }

            int expirationInHour = 24;

            DateTime expiratiorn = DateTime.UtcNow.AddHours(expirationInHour);

            var token = tokenProvider.CreateToken(user, expiratiorn);

            return Ok(new
            {
                token = token,
                expires_in = expirationInHour * 60 * 60
            });
        }
    }
}

