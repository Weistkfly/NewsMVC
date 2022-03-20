using Microsoft.AspNetCore.Mvc;
using NewsMVC2019_2820.Data;
using NewsMVC2019_2820.Models;
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
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }  

        [HttpPost]
        public IActionResult Create(News obj)
        {
            if (ModelState.IsValid)
            {
                obj.Date = DateTime.Now;
                _db.News.Add(obj);
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

        [HttpPost]
        public IActionResult Update(News obj)
        {
            if (ModelState.IsValid)
            {
                _db.News.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
