using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    public class SizeController : Controller
    {
        #region Размер

        private ApplicationContext db;

        public SizeController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        //public async Task<IActionResult> SizeView()
        //{
        //    return View(await db.Sizes.ToListAsync());
        //}
        public IActionResult CreateSize()
        {
            return View();
        }
        [HttpPost]
        //public async Task<IActionResult> CreateSize(Size size)
        //{
        //    db.Sizes.Add(size);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("SizeView");
        //}
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Material material = await db.Materials.FirstOrDefaultAsync(predicate => predicate.Id == id);
                /*  foreach (Employee employee in db.Employees.Where(predicate => predicate.RoleId == id))
                  {
                      employee.RoleId = null;
                      db.Employees.Update(employee);
                      *//*                    await db.SaveChangesAsync();
                                          await db.DisposeAsync();*//*
                  }
                  if (role != null)
                  {
                      db.Roles.Remove(role);
                      await db.SaveChangesAsync();
                      return RedirectToAction("Index1");
                  }*/
            }
            return NotFound();
        }
        #endregion
    }
}

