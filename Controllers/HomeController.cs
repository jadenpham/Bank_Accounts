using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAccount.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace BankAccount.Controllers
{
    public class HomeController : Controller
    {
        private MyContext DbContext;

        public HomeController(MyContext context)
        {
            DbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(UserReg newUser)
        {
            if(ModelState.IsValid)
            {
                if(DbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("Index");
                };

                PasswordHasher<UserReg> Hasher = new PasswordHasher<UserReg>();
                newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                DbContext.Add(newUser);
                DbContext.SaveChanges();
                HttpContext.Session.SetInt32("UserId", newUser.UserId);
                return View("Account");
            }
            else
            {

                return View("Index");
            }
        }

        [HttpGet("login")]
        public IActionResult LoginPage() => View();


        [HttpPost("verify")]
        public IActionResult Login(UserLog LogForm)
        {
            if(ModelState.IsValid)
            {
                var userInDb = DbContext.Users.FirstOrDefault(u => u.Email == LogForm.Email);
                if(userInDb == null)
                {
                    ModelState.AddModelError("Email", "Invalid login");
                    return View("LoginPage");
                }
                var hasher = new PasswordHasher<UserLog>();
                var result = hasher.VerifyHashedPassword(LogForm, userInDb.Password, LogForm.Password);
                if (result ==0)
                {
                    ModelState.AddModelError("Password", "Invalid Login");
                    return View("LoginPage");
                }

                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Account", new {id = userInDb.UserId});

            }
            else{
                return View("LoginPage");
            }
        }
        [HttpGet("account/{id}")]
        public IActionResult Account()
        {
            int? UserSess = HttpContext.Session.GetInt32("UserId");
            if(UserSess == 0 || UserSess == null)
            {
                return View("LoginPage");
            }

            UserTransComb converted = new UserTransComb();
            converted.NewUser =  DbContext.Users
                            .Include(u => u.TransMade)
                            .FirstOrDefault(u => u.UserId == UserSess);
            ViewBag.trans = converted.NewUser.TransMade.OrderByDescending(t => t.CreatedAt);
            Transactions Sum = new Transactions();
            Sum.Amount = 0;
            foreach(var tran in converted.NewUser.TransMade)
            {
                Sum.Amount += tran.Amount;
            }
            ViewBag.Balance= Math.Round(Sum.Amount,2);
            return View(converted);
        }

        [HttpPost("trans")]
        public IActionResult Transactions(Transactions newTrans)
        {
            var user = DbContext.Users.Include(u => u.TransMade).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            Transactions trans = new Transactions();
            if(ModelState.IsValid)
            {
                trans.Amount = newTrans.Amount;
                user.TransMade.Add(trans);
                DbContext.SaveChanges();
                return RedirectToAction("Account", new {id = user.UserId});
            }
            else{
                ModelState.AddModelError("NewTrans.Amount", "Invalid amount");
                TempData["Error"] = "Invalid amount";
                return RedirectToAction("Account", new {id = user.UserId});
            }
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
