using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Create(User user)
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

        public async Task<IActionResult> History()
        {
            var fileModels = new List<FileModel>();
            var history = new List<History>();
            var cart = await db.Carts.Include(cart => cart.Order).Where(predicate=>predicate.Order.DeliveryStatus==true)
                .Where(predicate => predicate.Order.UserId == Convert.ToInt32(HttpContext.Request.Cookies["ID"]))
                .Include(cart=>cart.Product).ToListAsync();
            if (cart != null)
            {
                foreach (var items in cart)
                {
                    Product product = db.Products.FirstOrDefault(p => p.Id == items.ProductId);
                    Cart cart1 = db.Carts.FirstOrDefault(c => c.Id == items.Id);
                    FileModel fileModel = new FileModel();
                    fileModel = null;
                    if (product!=null)
                        fileModel = db.FileModels.FirstOrDefault(f => f.ProductId == product.Id);

                    if (fileModel != null)
                    {
                        History history1 = new History { Id = fileModel.Id, CartID = cart1.Id, Cart = cart1, FileModel = fileModel, FileModelId = fileModel.Id };
                        history.Add(history1);
                        FileModel fileModel1 = new FileModel { Id = fileModel.Id, Name = fileModel.Name, Path = fileModel.Path, ProductId = fileModel.ProductId };
                        fileModels.Add(fileModel1);
                    }


                }

                return View(history);
            }
            return NotFound();
        }
        #region Изменение
        public async Task<ActionResult> Profile()
        {
            if (HttpContext.Request.Cookies["ID"] != null)
            {
                var user = await db.Users.FirstOrDefaultAsync(predicate => predicate.Id == Convert.ToInt32(HttpContext.Request.Cookies["ID"]));
                if (user != null)
                {
                    return View(user);
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
                return RedirectToAction("Index", "FileModel");
            }
            else
            {
                return View("Profile", user);
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
