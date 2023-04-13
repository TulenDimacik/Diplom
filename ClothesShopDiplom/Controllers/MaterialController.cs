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
    [Authorize(Roles = "Товаровед")]
    public class MaterialController : Controller
    {
        #region Материал
        
        private ApplicationContext db;

        public MaterialController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        public async Task<IActionResult> MaterialView()
        {
            return View(await db.Materials.ToListAsync());
        }
        public IActionResult CreateMaterial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMaterial(Material material)
        {
            db.Materials.Add(material);
            await db.SaveChangesAsync();
            return RedirectToAction("MaterialView");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Material material = await db.Materials.FirstOrDefaultAsync(predicate => predicate.Id == id);
                foreach (Product product in db.Products.Where(predicate => predicate.MaterialId == id))
                {
                    product.ProductTypeId = null;
                    db.Products.Update(product);
                }
                if (material != null)
                {
                    db.Materials.Remove(material);
                    await db.SaveChangesAsync();
                    return RedirectToAction("MaterialView");
                }

            }
            return NotFound();
        }
        #endregion
    }
}

