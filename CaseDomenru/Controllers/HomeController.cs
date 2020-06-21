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
        public HomeController(CaseDomenruDB dbcontext)
        {
            db = dbcontext;
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                MailAddress from = new MailAddress("unnamed2@тестовая-зона.рф");
                MailAddress to = new MailAddress(model.Input);

                MailMessage message = new MailMessage(from, to);
                message.Subject = "тестовое письмо от " + DateTime.Now;
                message.Body = "<div><b>Ваши оценки:</b></div>" +
                    "<p>" +
                    "<table> " +
                    "<b><tr><td>Предмет</td><td>I ч.</td><td>II ч.</td><td>III ч.</td><td>IV ч.</td><td>Итог</td><b><tr/>" +
                    "<tr><td>Русский язык</td><td>5</td><td>4</td><td>2</td><td>2</td><td>3</td><tr/>" +
                    "<tr><td>Математика</td><td>5.</td><td>4</td><td>5</td><td>5</td><td>5</td><tr/>" +
                    "</table>" +
                    "</p>";
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("test.maeeil12345.6789.0.123456789.0@gmail.com", "bfd20380a6");
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
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
