using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    [Authorize(Roles = "Товаровед")]
    public class ProductController : Controller
    {
        private ApplicationContext db;
        IWebHostEnvironment _appEnvironment;


        public ProductController(ApplicationContext applicationContext, IWebHostEnvironment appEnvironment)
        {
            db = applicationContext;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> ProductView()
        {
            var products = db.Products.Include(p => p.Material).Include(p=> p.ProductType);
            return View(await products.ToListAsync());
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(predicate => predicate.Id == id);
                foreach (FileModel fileModel in db.FileModels.Where(predicate => predicate.ProductId == id))
                {
                   
                    db.FileModels.Remove(fileModel);
                }
                foreach (Cart cart in db.Carts.Where(predicate => predicate.ProductId == id))
                {
                    cart.ProductId = null;
                    db.Carts.Update(cart);
                }
                if (product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ProductView");
                }
            }
            return NotFound();
        }
        public IActionResult ProductCreate()
        {
            Zagruzka();
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ProductCreate(Product product, IFormFileCollection uploadedFile)
        {
            
            var products = db.Products.Where(p => EF.Functions.Like(p.ProductName, product.ProductName));

            if (products.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();

                    foreach (var file in uploadedFile)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        FileModel fileModel = new FileModel { Name = fileName, Path = "/Files/" + fileName, ProductId = product.Id };
                        db.FileModels.Add(fileModel);
                    }

                    await db.SaveChangesAsync();
                    return RedirectToAction("ProductView");
                }
            }
            else
                    ViewBag.ProductUnique = "Товар с таким названием уже существует";
            Zagruzka();
            return View();
        }
      
        public async Task<ActionResult> ProductEdit(int? id)
        {

            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(predicate => predicate.Id == id);
                Zagruzka();
                if (product != null)
                {
                    return View(product);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(Product product)
        {
            /*em.Id = employee.Id;*/

            var products = db.Products.Where(p => EF.Functions.Like(p.ProductName, product.ProductName)).Where(p => p.Id != product.Id); 
            if (ModelState.IsValid)
                if (products.Count() == 0)
                {
                    db.Products.Update(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("ProductView");
                }
                Zagruzka();
                return View();
        }

        public void Zagruzka()
        {
            List<ProductType> productTypes = new List<ProductType>();
            foreach (var vid in db.ProductTypes)
            {
                var productTypes1 = new ProductType { Id = vid.Id, ProductTypeName = vid.ProductTypeName };
                productTypes.Add(productTypes1);
            }
            ViewBag.ProductTypes = new SelectList(productTypes, "Id", "ProductTypeName");

            List<Material> materials = new List<Material>();
            foreach (var material in db.Materials)
            {
                var material1 = new Material { Id = material.Id, MaterialName = material.MaterialName };
                materials.Add(material1);
            }
            ViewBag.Materials = new SelectList(materials, "Id", "MaterialName");
        }
    }
}
