using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI2019_2820.Data;
using NewsAPI2019_2820.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI2019_2820.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public NewsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<News>> AddArticles(News model)
        {
            var news = new News()
            {
                Title = model.Title,
                Content = model.Content,
                Date = model.Date,
                AuthorId = model.AuthorId,
                CountryId = model.CountryId,
                CategoryId = model.CategoryId,
            };

            await _db.News.AddAsync(news);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByNewTitle), new { q = news.Title }, news);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var query = _db.News.AsQueryable();

            var articles = query.Select(articles => new NewsDTO
            {
                Title = articles.Title,
                Content = articles.Content,
                Date = articles.Date,
                Author = articles.Author.Name,
                Category = articles.Category.Name,
                Country = articles.Country.Name
            }).ToArray();

            return Ok(articles);
        }

        [HttpGet("{q}")]
        [AllowAnonymous]
        public IActionResult GetByNewTitle(string q)
        {
            var query = _db.News.AsQueryable().Where(a => a.Title.Contains(q));

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(a => a.Title.Contains(q));
                var articles = query.Select(articles => new NewsDTO
                {
                    Title = articles.Title,
                    Content = articles.Content,
                    Date = articles.Date,
                    Author = articles.Author.Name,
                    Category = articles.Category.Name,
                    Country = articles.Country.Name
                }).ToArray();

                return Ok(articles);
            }

            return NotFound();
        }

        [HttpGet("{q}")]
        [AllowAnonymous]
        public IActionResult GetByCategory(string q)
        {
            var query = _db.News.AsQueryable().Where(a => a.Category.Name == q);

            if (!string.IsNullOrEmpty(q))
            {
                var articles = query.Select(articles => new NewsDTO
                {
                    Title = articles.Title,
                    Content = articles.Content,
                    Date = articles.Date,
                    Author = articles.Author.Name,
                    Category = articles.Category.Name,
                    Country = articles.Country.Name
                }).ToArray();

                return Ok(articles);
            }

            return NotFound();
        }

        [HttpGet("{q}")]
        [AllowAnonymous]
        public IActionResult GetByCountry(string q)
        {
            var query = _db.News.AsQueryable().Where(a => a.Country.Name == q);

            if (!string.IsNullOrEmpty(q))
            {
                var articles = query.Select(articles => new NewsDTO
                {
                    Title = articles.Title,
                    Content = articles.Content,
                    Date = articles.Date,
                    Author = articles.Author.Name,
                    Category = articles.Category.Name,
                    Country = articles.Country.Name
                }).ToArray();

                return Ok(articles);
            }

            return NotFound();
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutNew(int id, News New)
        {
            if (id != New.Id)
            {
                return BadRequest();
            }

            _db.Entry(New).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!IsNewThere(id))
                    return NotFound();

                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var news = await _db.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }

            _db.News.Remove(news);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool IsNewThere(int id)
        {
            return _db.News.Any(a => a.Id == id);
        }
    }
}
