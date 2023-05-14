using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Authorize(Roles = "Администратор")]
    public class RoleController : Controller
    {
        #region Роль
        //хуйня для бд
        private ApplicationContext db;

        public RoleController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
  
        public async Task<IActionResult> Index1()
        {
            return View(await db.Roles.ToListAsync());
        }
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(Role role)
        {
            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return RedirectToAction("Index1");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Role role = await db.Roles.FirstOrDefaultAsync(predicate => predicate.Id == id);
                foreach (Employee employee in db.Employees.Where(predicate => predicate.RoleId == id))
                {
                    employee.RoleId = null;
                    db.Employees.Update(employee);
                    /*                    await db.SaveChangesAsync();
                                        await db.DisposeAsync();*/
                }
                if (role != null)
                {
                    db.Roles.Remove(role);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index1");
                }
            }
            return NotFound();
        }
        #endregion
    }
}
