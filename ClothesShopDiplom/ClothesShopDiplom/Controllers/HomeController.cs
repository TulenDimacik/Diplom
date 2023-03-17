using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    public class HomeController : Controller
    {
        //хуйня для бд
        private ApplicationContext db;

        public HomeController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }

        [HttpPost]
        public IActionResult Login(Employee employee)
        {
            //в скобочках то что мы передаем то есть вводим в поля
            //Ищем логин в бд
            var st = db.Employees.FirstOrDefault(p => p.Login == employee.Login);
            if (st != null)
            {
                //подтверждаем пароль 
                bool verify = BCrypt.Net.BCrypt.Verify(employee.Password, st.Password);
                if (verify == true)
                {
                        return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("Create");
            }
             return View();
            
            /*
                        users = db.Users.Where(p => EF.Functions.Like(p.Login, user.Login, user.Email) && EF.Functions.Like(p.Password, user.Password) && EF.Functions.Like(p.RolesId.ToString(), "2"));

                        foreach (User user2 in users)
                        {
                            ff = user2.Id;
                            login2 = true;
                        }*/
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
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
