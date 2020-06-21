using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CaseDomenru.Data;
using CaseDomenru.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CaseDomenru.Controllers
{
    public class UserController : Controller
    {
        private CaseDomenruDB db;
        public UserController(CaseDomenruDB dbcontext)
        {
            db = dbcontext;
        }

        public IActionResult Index()
        {
            if (User.Claims.ToList()[0].Value != null)
            {
                return Index();
            } 
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult Authorization()
        {
            return View(new AuthorizationModel() { LoginModel = new Models.LoginModel(), RegistrationModel = new Models.RegistrationModel() });
        }

        [HttpPost]
        public IActionResult Authorization(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                
            }
            return RedirectToAction("Authorization", new AuthorizationModel() { LoginModel = model, RegistrationModel = new Models.RegistrationModel() });
        }

        [HttpPost]
        public IActionResult Authorization(Models.RegistrationModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction("Authorization", new AuthorizationModel() { RegistrationModel = model, LoginModel = new Models.LoginModel() });
        }

        public void Authenticate(string email, string password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
