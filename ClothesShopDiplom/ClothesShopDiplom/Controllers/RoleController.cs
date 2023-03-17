using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
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
        #endregion
    }
}
