using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVC2019_2820.Data;
using NewsMVC2019_2820.Models;
using NewsMVC2019_2820.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsMVC2019_2820.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        } 

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (model.Password != model.PasswordConfirm)
            {
                ModelState.
                    AddModelError(nameof(model.Password), "Password doesn't match");
            }

            if (ModelState.IsValid)
            {
                var user = new User();
                user.Username = model.Username;
                user.Password = model.Password;                
                
                _db.Users.Add(user);
                _db.SaveChanges();

                return RedirectToAction("Login");
                
            }

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users
                    .Include(x => x.Roles)
                    .Where(x => x.Username == model.Username)
                    .FirstOrDefault();

                if (model.Password == user.Password)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, model.Username));

                    if (user.Roles != null)
                    {
                        foreach (var item in user.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, item.Name));
                        }
                    }

                    var identity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity), 
                        new AuthenticationProperties());

                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        var returnURl = Request.Query["ReturnUrl"].ToString();
                        return Redirect(returnURl);
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
