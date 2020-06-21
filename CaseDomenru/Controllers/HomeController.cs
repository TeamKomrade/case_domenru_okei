using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CaseDomenru.Models;
using System.Net.Mail;
using System.Net;
using CaseDomenru.Data;

namespace CaseDomenru.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CaseDomenruDB db;
        public HomeController(ILogger<HomeController> logger, CaseDomenruDB dbcontext)
        {
            _logger = logger;
            db = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Validate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Validate(ValidationModel model)
        {
            if (ModelState.IsValid)
            {
                model.isValidEmail = NameValidation.ValidateEmail(model.Input);
                return View(model);
            }
            else return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
