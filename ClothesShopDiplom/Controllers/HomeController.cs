using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    public class HomeController : Controller
    {
       
        private ApplicationContext db;
        private IHttpContextAccessor _httpContextAccessor;
        int rand;
        
        public HomeController(ApplicationContext applicationContext, IHttpContextAccessor httpContextAccessor)
        {
            db = applicationContext;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "FileModel");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Auth auth)
        {
            var emlog = db.Employees.Include(em => em.Role).SingleOrDefault(p => p.Login == auth.Login);
            var uslog = db.Users.FirstOrDefault(p => p.Login == auth.Login);

            if (emlog != null)
            {
             
                bool verify = BCrypt.Net.BCrypt.Verify(auth.Password, emlog.Password);
                if (verify == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, emlog.Login),
                        new Claim(ClaimTypes.Role, emlog.Role.RoleName)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                  

                    if (emlog.Role.RoleName=="Администратор")
                     return RedirectToAction("Index2", "Employee");

                    else if (emlog.Role.RoleName == "Товаровед")
                        return RedirectToAction("ProductView", "Product");
                    
                    else
                        return RedirectToAction("Order", "FileModel");
                }
                else
                {
                    ViewBag.AuthError = "Неверный логин или пароль";
                    return View();
                }    
                   
            }
            else if(uslog!=null)
            {
                bool verify = BCrypt.Net.BCrypt.Verify(auth.Password, uslog.Password);
                if (verify == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, uslog.Login),
                        new Claim(ClaimTypes.Role, "Пользователь")
                        
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                   

                    if (_httpContextAccessor.HttpContext.Request.Cookies["ID"] != null)
                    {
                        Response.Cookies.Delete("ID");
                    }
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Append("ID", uslog.Id.ToString(), cookie);

                    return RedirectToAction("Index", "FileModel");
                }
                else
                {
                    ViewBag.AuthError = "Неверный логин или пароль";
                    return View();
                }
            }
            ViewBag.AuthError = "Такой аккаунт не существует";
            return View();
        
        }
        
        
        public IActionResult Login()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            //var a = User.Identity.IsAuthenticated;
            //var b = User.IsInRole("Admin");
            return View();
        }

        public IActionResult Recovery()
        {
            return View();
        }
      
        public IActionResult ConfirmCode()
        {        
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                if (TempData["idEmployee"] != null && TempData["idUser"] == null)
                {
                    Employee em = new Employee();
                    em = await db.Employees.FirstOrDefaultAsync(predicate => predicate.Id == (int)TempData["idEmployee"]);
                    em.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.ConfirmNewPassword);
                    db.Employees.Update(em);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index2", "Employee");

                }
                else if(TempData["idUser"] != null && TempData["idEmployee"] == null)
                {
                    User user = new User();
                    user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == (int)TempData["idUser"]);
                    user.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.ConfirmNewPassword);
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Login");
                }
                else
                {
                    User user = new User();
                    user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == Convert.ToInt32( _httpContextAccessor.HttpContext.Request.Cookies["ID"]));
                    user.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.ConfirmNewPassword);
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Profile", "User");
                }

                
            }

            return View();
        }

        [HttpPost]
        public  IActionResult ConfirmCode(string code)
        {
           
            
            if (TempData["code"] != null && TempData.ContainsKey("code"))
            {

                string emailCode = Convert.ToString(TempData["code"]);
                if (code == emailCode)
                {
                    return RedirectToAction("ResetPassword");
                }
                else
                {
                    ViewBag.ConfirmError = "Неверный код";
                    return View();
                }
            }
            else
            {
                string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["code1"];
                if (code == cookieValue)
                {
                    return RedirectToAction("ResetPassword");
                }
                else
                {
                    ViewBag.ConfirmError = "Неверный код";
                    return View();
                }
            }
       
        }

        [HttpPost]
        public IActionResult Recovery(Recovery recovery)
        {
            var emlog = db.Employees.FirstOrDefault(p => p.Email == recovery.Email);
            var uslog = db.Users.FirstOrDefault(p => p.Email == recovery.Email);
            if (emlog != null || uslog != null)
            {
                try
                {
                    Random r = new Random();
                    rand = r.Next(100000, 999999);
                    MailAddress from = new MailAddress("isip_d.a.gordyushin@mpt.ru", "Магазин одежды");
                    MailAddress to = new MailAddress(recovery.Email);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Восстановление пароля";
                    m.IsBodyHtml = false;
                    m.Body = Convert.ToString(rand);
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("isip_d.a.gordyushin@mpt.ru", "fvpzqutqpjdbwimp");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
                catch
                {
                    return NotFound();
                }
            }
            else
            {
                ViewBag.RecoveryError = "Проверьте введенную почту";
                return View();
            }
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMinutes(2);
            Response.Cookies.Append("code1", rand.ToString(), cookie);
            if (emlog != null)
            {
                TempData["idEmployee"] = emlog.Id;
            
            }
            else
            {
                TempData["idUser"] = uslog.Id;
           
            }
            TempData["code"] = rand.ToString();

          
            return RedirectToAction("ConfirmCode" ) ;
        }
  

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
