using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseDomenru.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;

namespace CaseDomenru.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Authorization()
        {
            return View(new AuthorizationModel() { LoginModel = new Models.LoginModel(), RegistrationModel = new Models.RegistrationModel() });
        }

        [HttpPost]
        public IActionResult Authorization(Models.LoginModel model)
        {
            return RedirectToAction("Authorization", new AuthorizationModel() { LoginModel = model, RegistrationModel = new Models.RegistrationModel() });
        }

        [HttpPost]
        public IActionResult Authorization(Models.RegistrationModel model)
        {
            return RedirectToAction("Authorization", new AuthorizationModel() { RegistrationModel = model, LoginModel = new Models.LoginModel() });
        }
    }
}
