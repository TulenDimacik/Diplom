using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class EmployeeController : Controller
    {
        private ApplicationContext db;
        Employee em = new Employee();
      

        public EmployeeController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        #region  Сотрудник
      
       
        public async Task<IActionResult> Index2()
        {
        
            var employees = db.Employees.Include(p => p.Role);
            return View(await employees.ToListAsync());
        }

     
        public IActionResult Create()
        {
                Zagruzka();
                return View(em);
        }

    
        [HttpPost]
        
        public async Task<IActionResult> Create(Employee employee)
        {
            if (Check(employee) == true)
            {

                employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }
            else
            {
                Zagruzka();
                return View(em);
            }
           
        }
        public void Zagruzka()
        {
                List<Role> roles1 = new List<Role>();
                em.RolesList = new List<Role> { };
                foreach (var roles in db.Roles)
                {
                    var role = new Role { Id = roles.Id, RoleName = roles.RoleName };
                    roles1.Add(role);
                }
                foreach (var items in roles1)
                {
                    em.RolesList.Add(items);
                }
        }

        //Удаление
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Employee user = await db.Employees.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (user != null)
                {
                    db.Employees.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index2");
                }
            }
            return NotFound();
        }
        //изменение
       
        public async Task<ActionResult> Edit(int? id)
        {

            if (id != null)
            { 
                em = await db.Employees.FirstOrDefaultAsync(predicate => predicate.Id == id);
                Zagruzka();
                if (em != null)
                {
                    return View(em);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            em.Id = employee.Id;
      
            if (Check(employee) == true)
            {
                db.Employees.Update(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index2");
            }
            else
            {
                Zagruzka();
                return View(em);
            }
        }
        #endregion
        public bool Check(Employee user)
        {
            bool Log1 = false;
            bool Log2 = false;
            var userlogin = db.Employees.Where(p => EF.Functions.Like(p.Login, user.Login)).Where(p => p.Id != user.Id);
            var empllogin = db.Users.Where(p => EF.Functions.Like(p.Login, user.Login));
            var useremail = db.Employees.Where(p => EF.Functions.Like(p.Email, user.Email)).Where(p => p.Id != user.Id);
            var emplemail = db.Users.Where(p => EF.Functions.Like(p.Email, user.Email));

            if (userlogin.Count() != 0 || empllogin.Count() != 0)
            {
                Log1 = true;
                ViewBag.lol = user.Email;
                ViewBag.LoginError = "Такой логин уже существует";
            }
            if (useremail.Count() != 0 || emplemail.Count() != 0)
            {
                Log2 = true;
                ViewBag.lol = user.Email;
                ViewBag.EmailError = "Эта почта уже занята";
            }
            if (Log1 == false && Log2 == false && ModelState.IsValid)
                return true;
            else
                return false;
        }
    }
}
