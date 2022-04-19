using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI2019_2820.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI2019_2820.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CountryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _db.Countries.ToListAsync();
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _db.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _db.Entry(country).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            var newsCountry = new Country
            {
                Name = country.Name
            };

            _db.Countries.Add(newsCountry);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = newsCountry.Id }, newsCountry);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _db.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _db.Countries.Remove(country);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _db.Countries.Any(e => e.Id == id);
        }
    }
}

