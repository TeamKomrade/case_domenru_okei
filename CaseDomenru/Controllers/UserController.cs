using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using CaseDomenru.Data;
using CaseDomenru.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            string email;
            if (User.Identity.IsAuthenticated && (email = User.Claims.ToList()[0].Value) != null)
            {
                email = Utils.GetHash(User.Claims.ToList()[0].Value);
                var UVM = new UserViewModel();
                UVM.User = db.Users.Where(u => u.Login == email).Include(u => u.Person).Include(u => u.UniqueCode).FirstOrDefault();
                return View(UVM);
            } 
            else return RedirectToAction("Index", "Home");
        }

        public IActionResult Authorization()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string login, password;
                login = Utils.GetHash(model.Email);
                password = Utils.GetHash(model.Password);
                var user = db.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();
                if (user != null)
                {

                    Authenticate(model.Email, model.Password);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login", model);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Models.RegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                if (!NameValidation.ValidateEmail(model.Email))
                {
                    ModelState.AddModelError("", "Введённый почтовый адрес не прошёл валидацию.");
                    return RedirectToAction("Registration", model);
                }
                db.Users.Add(new Data.User()
                {
                    Email = NameValidation.idn.GetAscii(model.Email),
                    Login = Utils.GetHash(model.Email),
                    Password = Utils.GetHash(model.Password),
                    Person = new Person()
                    {
                        DateOfBirth = DateTime.Now,
                        FirstName = "Тест",
                        LastName = "Кейсов",
                        Patronymic = "Тестович"
                    },
                    Role = Roles.Ученик,
                    UniqueCode = new UniqueKey()
                    {
                        UniqueKeyString = Utils.GetHash(DateTime.Now.ToShortTimeString())
                    }
                });
                db.SaveChanges();
                MailAddress from = new MailAddress("unnamed2@тестовая-зона.рф");
                MailAddress to = new MailAddress(model.Email);

                MailMessage message = new MailMessage(from, to);
                message.Subject = "Благодарим за регистрацию на сайте ИТ-Дневник!" + DateTime.Now;
                message.Body = $"Вы зарегистрировались по уникальному ключу {model.UniqueKey}" ;
                message.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("test.maeeil12345.6789.0.123456789.0@gmail.com", "bfd20380a6");
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                Authenticate(model.Email, model.Password);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Registration", model);
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
