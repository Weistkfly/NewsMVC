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
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _db.Authors.Select(item => new Author
            {
                Id = item.Id,
                Name = item.Name
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _db.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _db.Entry(author).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsAuthorThere(id))
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
        [AllowAnonymous]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {

            var newAuthor = new Author
            {
                Name = author.Name
            };

            _db.Authors.Add(newAuthor);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = newAuthor.Id }, newAuthor);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool IsAuthorThere(int id)
        {
            return _db.Authors.Any(e => e.Id == id);
        }

    }
}
