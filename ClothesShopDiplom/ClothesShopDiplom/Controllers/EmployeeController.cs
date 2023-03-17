using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    public class EmployeeController : Controller
    {


        //хуйня для бд
        private ApplicationContext db;
        Employee em = new Employee();
      
        public EmployeeController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        #region  Сотрудник
        //получение данных из бд
        public async Task<IActionResult> Index2()
        {
            //Блять я ебу короче сначала добавили в Employee атрибут типа навигационный какой то я ебу а потом строчкой ниже включили видимо 
            //модель и потом на верстке через DisplayFor смогли получить данные охуеть
            var employees = db.Employees.Include(p => p.Role);
            return View(await employees.ToListAsync());
        }

        //Ретурн Вьюшки
        public IActionResult Create()
        {
                Zagruzka();
                return View(em);
        }

        //Добавление данных получается
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            bool Log1 = false;
            bool Log2 = false;
            var empl = db.Employees.Where(p => EF.Functions.Like(p.Login, employee.Login));
            var empl1 = db.Employees.Where(p => EF.Functions.Like(p.Email, employee.Email));
            
            foreach (Employee employee1 in empl)
            {
                Log1 = true;
                ViewBag.LoginError = "Такой логин уже существует";
            }

            foreach (Employee employee2 in empl1)
            {
                Log2 = true;
                ViewBag.EmailError = "Эта почта уже занята";
            }

            if (Log1 == true || Log2 == true)
            {
                Zagruzka();
                return View(em);
            }

            else
            {
                    if (ModelState.IsValid)
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
        }
        public void Zagruzka()
        {
                List<Role> roles1 = new List<Role>();
                //em = new Employee();
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
         /*       em.Id = (int)id;*/
                Zagruzka();
                /* 
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
                }*/
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
            bool Log1 = false;
            bool Log2 = false;
            var empl = db.Employees.Where(p => EF.Functions.Like(p.Login, employee.Login)).Where(p=>p.Id!=employee.Id);
            var empl1 = db.Employees.Where(p => EF.Functions.Like(p.Email, employee.Email)).Where(p => p.Id != employee.Id);
            foreach (Employee employee1 in empl)
            {
                Log1 = true;
                ViewBag.LoginError = "Такой логин уже существует";
            }

            foreach (Employee employee2 in empl1)
            {
                Log2 = true;
                ViewBag.EmailError = "Эта почта уже занята";
            }

            if (Log1 == true || Log2 == true)
            {
                Zagruzka();
                return View(em);
            }

            else
            {
                //employee.Password = "123";
                if (ModelState.IsValid)
                {
                    db.Employees.Update(employee);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index2");
                }
                else
                {
                   // em.Id = employee.Id;
                    Zagruzka();
                    return View(em);
                }

            }
        }
        #endregion
    }
}
