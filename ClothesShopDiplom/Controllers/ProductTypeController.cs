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
    public class ProductTypeController : Controller
    {
        #region Вид одежды

        private ApplicationContext db;

        public ProductTypeController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        public async Task<IActionResult> ProductTypeView()
        {
            return View(await db.ProductTypes.ToListAsync());
        }
        public IActionResult CreateProductType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductType(ProductType productType)
        {
            db.ProductTypes.Add(productType);
            await db.SaveChangesAsync();
            return RedirectToAction("ProductTypeView");
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {

            if (id != null)
            {
                ProductType productType = await db.ProductTypes.FirstOrDefaultAsync(predicate => predicate.Id == id);
                foreach (Product product in db.Products.Where(predicate => predicate.ProductTypeId == id))
                {
                    product.ProductTypeId = null;
                    db.Products.Update(product);
                }
                if (productType != null)
                {
                    db.ProductTypes.Remove(productType);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ProductTypeView");
                }
           
            }
            return NotFound();
        }
        #endregion
    }
}

