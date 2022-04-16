using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsMVC2019_2820.Data;
using NewsMVC2019_2820.Models;
using NewsMVC2019_2820.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsMVC2019_2820.Controllers
{
    public class newsArticleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public newsArticleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<News> objList = _db.News;
            foreach(var obj in objList)
            {
                obj.Author = _db.Authors.FirstOrDefault(u => u.Id == obj.AuthorId);
                obj.Category = _db.Categories.FirstOrDefault(u => u.Id == obj.CategoryId);
                obj.Country = _db.Countries.FirstOrDefault(u => u.Id == obj.CountryId);
            }
           // var sortedObjtList = objList.
            return View(objList);
        }

        public IActionResult Create()
        {
            NewsVM authorVM = new NewsVM()
            {
                News = new News(),
                AuthorDropDown = _db.Authors.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CategoryDropDown = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CountryDropDown = _db.Countries.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(authorVM);
        }  

        [HttpPost]
        public IActionResult Create(NewsVM obj)
        {
            if (ModelState.IsValid)
            {
                obj.News.Date = DateTime.Now;
                _db.News.Add(obj.News);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.News.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.News.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.News.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        public IActionResult Update(int? id)
        {
            NewsVM authorVM = new NewsVM()
            {
                News = new News(),
                AuthorDropDown = _db.Authors.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CategoryDropDown = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CountryDropDown = _db.Countries.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            authorVM.News = _db.News.Find(id);

            if (authorVM.News == null)
            {
                return NotFound();
            }
            return View(authorVM);
        }

        [HttpPost]
        public IActionResult Update(NewsVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.News.Update(obj.News);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
//NONSENSE