using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    public class UserController : Controller
    {
        private ApplicationContext db;
        public UserController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        #region Добавление
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> UserIndex()
        {
            var users = db.Users;
            return View(await users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user )
        {
            if (Check(user) == true)
            {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Login","Home");
            }
            else
            {
                return View();
            }

        }
        #endregion

        #region Изменение
        public async Task<ActionResult> Edit(int? id)
        {
            if (id != null)
            {
             var users = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (users != null)
                {
                    return View(users);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {

            if (Check(user) == true)
            {
                db.Users.Update(user);
                await db.SaveChangesAsync();
                return RedirectToAction("UserIndex");
            }
            else
            {
                return View(user);
            }

        }
        #endregion
        public bool Check(User user)
        {
            bool Log1 = false;
            bool Log2 = false;
            var userlogin = db.Users.Where(p => EF.Functions.Like(p.Login, user.Login)).Where(p => p.Id != user.Id);
            var empllogin = db.Employees.Where(p => EF.Functions.Like(p.Login, user.Login));
            var useremail = db.Users.Where(p => EF.Functions.Like(p.Email, user.Email)).Where(p => p.Id != user.Id);
            var emplemail = db.Employees.Where(p => EF.Functions.Like(p.Email, user.Email));

            if (userlogin.Count() != 0 || empllogin.Count() != 0)
            {
                Log1 = true;
                ViewBag.LoginError = "Такой логин уже существует";
            }
            if (useremail.Count() != 0 || emplemail.Count() != 0)
            {
                Log2 = true;
                ViewBag.EmailError = "Эта почта уже занята";
            }
            if (Log1 == false && Log2 == false && ModelState.IsValid)
                return true;
            else
                return false;
        }

    }
}
