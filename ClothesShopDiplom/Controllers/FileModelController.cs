using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using PagedList.Mvc;
using PagedList;

namespace ClothesShopDiplom.Controllers
{
    public class FileModelController : Controller
    {
        ApplicationContext db;
        IWebHostEnvironment _appEnvironment;
        private IHttpContextAccessor _httpContextAccessor;
        public FileModelController(ApplicationContext context, IWebHostEnvironment appEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            db = context;
            _appEnvironment = appEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFromCart(int? id)
        {
            if (id != null)
            {
                Cart cart = await db.Carts.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if (cart != null)
                {
                    db.Carts.Remove(cart);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Cart");
                }
            }
            return NotFound();
        }

        public IActionResult FileModelIndex()
        {
            var files = db.FileModels.Include(p => p.Product);
            return View(files);
        }
        [Authorize(Roles = "Пользователь")]
        public IActionResult Cart()
        {
           
            var productIds = db.FileModels.Select(f => f.ProductId).Distinct().ToList();
            var fileModels = new List<FileModel>();
            var history = new List<History>();
            var carts = db.Carts.Include(p => p.Product).Include(p => p.Order)
               .Where(predicate => predicate.Order.UserId == Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Cookies["ID"]))
               .Where(predicate => predicate.Order.DeliveryStatus == false).ToList();
            foreach (var items in carts)
            {
                Product product = db.Products.FirstOrDefault(p => p.Id == items.ProductId);
                Cart cart1 = db.Carts.FirstOrDefault(c => c.Id == items.Id);
                FileModel fileModel = db.FileModels.FirstOrDefault(f => f.ProductId == product.Id);

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

        [Authorize(Roles = "Пользователь")]
        [HttpPost]
        public async Task< IActionResult> Cart(int? id, string act)
        {
            ViewBag.CountError = "";

            var history = new List<History>();
            if (id != null)
            {
                var productIds = db.FileModels.Select(f => f.ProductId).Distinct().ToList();
                var fileModels = new List<FileModel>();
               
                var carts = db.Carts.Include(p => p.Product).Include(p => p.Order)
                       .Where(predicate => predicate.Order.UserId == Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Cookies["ID"]))
                       .Where(predicate => predicate.Order.DeliveryStatus == false).ToList();
                foreach (var items in carts)
                {
                    Product product = db.Products.FirstOrDefault(p => p.Id == items.ProductId);
                    Cart cart1 = db.Carts.FirstOrDefault(c => c.Id == items.Id);
                    FileModel fileModel = db.FileModels.FirstOrDefault(f => f.ProductId == product.Id);

                    if (fileModel != null)
                    {
                        History history1 = new History { Id = fileModel.Id, CartID = cart1.Id, Cart = cart1, FileModel = fileModel, FileModelId = fileModel.Id };
                        history.Add(history1);
                        FileModel fileModel1 = new FileModel { Id = fileModel.Id, Name = fileModel.Name, Path = fileModel.Path, ProductId = fileModel.ProductId };
                        fileModels.Add(fileModel1);
                    }


                }

                var cart = await db.Carts.FirstOrDefaultAsync(predicate => predicate.Id == id);
                if(act=="inc")
                    cart.ProductAmount += 1;
                else if(act=="dec"&&cart.ProductAmount>1)
                    cart.ProductAmount -= 1;
                else
                {
                    ViewBag.CountError = "Количество не может быть меньше 1";
                    return View(history);
                }
                db.Carts.Update(cart);
                await db.SaveChangesAsync();
                return View(history);
            }
                

            return View(history);
        }

        [Authorize(Roles = "Пользователь")]
        [HttpGet]
        public IActionResult ConfirmOrder()
        {
            var user = db.Users
            .Where(p => p.Id == Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Cookies["ID"]))
            .Select(us => new { Name = us.Name, Surname = us.Surname })
            .FirstOrDefault();
            if (user != null)
            {
                ViewBag.UserName = user.Surname +" "+ user.Name;
                
            }
            return View();
        }
        [Authorize(Roles = "Пользователь")]
        [HttpPost]
        public async Task< IActionResult> ConfirmOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["ID"];
                var orderID = await db.Orders
        .Include(or => or.User)
        .Include(or => or.Employee)
        .Where(predicate => predicate.UserId.ToString() == cookieValue).Where(predicate => predicate.EmployeeId == null)
        .Where(predicate => predicate.DeliveryStatus == false).Where(predicate => predicate.DeliveryAddress == "")
        .Select(or => or.Id)
        .FirstOrDefaultAsync();
                var ord = await db.Orders.FirstOrDefaultAsync(predicate => predicate.Id == orderID);
                if (ord != null)
                {
                    ord.DeliveryAddress = order.DeliveryAddress;
                    ord.DeliveryDate = order.DeliveryDate;
                    ord.DeliveryStatus = true;
                    db.Orders.Update(ord);
                    await db.SaveChangesAsync();
                    var user = await db.Users.Where(p => p.Id.ToString() == cookieValue)
                        .Select(us => new {Name = us.Name, Surname = us.Surname, Email = us.Email}).FirstOrDefaultAsync();
                    MailAddress from = new MailAddress("isip_d.a.gordyushin@mpt.ru", "Магазин одежды");
                    MailAddress to = new MailAddress(user.Email);
                    MailMessage m = new MailMessage(from, to);
                    double totalprice = 0;
                    m.Subject = $"Заказ № {orderID}";
                    m.IsBodyHtml = true;
                    m.Body += $"Спасибо за ваш заказ,<b>{user.Name}</b>!<br>";
                    m.Body += $"<p style=\"color:red\">Письмо носит информационный характер и не является подтверждением заказа. В ближайшее время с Вами свяжется наш оператор для подтверждения.</p><br>";
                    m.Body += $"<table><tr><td><b>Товар</b></td><td style=\"padding-left: 25px\"><b>Размер</b></td><td style=\"padding-left: 25px\"><b>Количество</b></td> <td style=\"padding-left: 25px\"><b>Цена</b></td> </tr>";
                    foreach (var product in db.Carts.Include(or => or.Product).Where(ca=>ca.OrderId ==orderID).ToList())
                    {
                        m.Body += $@"<tr>
                    <td>{product.Product.ProductName}</td>
                    <td style='padding-left: 25px'>{product.Size}</td>
                    <td style='padding-left: 25px'>{product.ProductAmount}</td>
                    <td style='padding-left: 25px'>{product.Product.Price}</td>
                </tr>";
                         totalprice += product.Product.Price * product.ProductAmount;
                    }
                    m.Body += $"</table>";
                    m.Body += $"Сумма заказа: {totalprice}₽<br>";
                    m.Body += $"Адрес доставки: {order.DeliveryAddress} <br>";
                    m.Body += $"Дата доставки: <b>{order.DeliveryDate.ToString().Remove(order.DeliveryDate.ToString().Length-7)}</b> <br>";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("isip_d.a.gordyushin@mpt.ru", "fvpzqutqpjdbwimp");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                    return RedirectToAction("Index");
                }
                return View();
            }
            else
            {
                var user = db.Users
               .Where(p => p.Id == Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Cookies["ID"]))
               .Select(us => new { Name = us.Name, Surname = us.Surname })
               .FirstOrDefault();
                if (user != null)
                {
                    ViewBag.UserName = user.Surname + " " + user.Name;

                }
                return View(order);
            }
        }
        [Authorize(Roles = "Оператор")]
        public IActionResult Order()
        {
            var carts = db.Orders.ToList();
            int a = carts.Count();

            return View(carts);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsId = await  db.FileModels.FirstOrDefaultAsync(predicate => predicate.ProductId == id);

            var fileModel = await db.FileModels
                .Include(fm => fm.Product)
                    .ThenInclude(p => p.Material)
                .Include(fm => fm.Product)
                    .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(fm => fm.Id == productsId.Id);

            if (fileModel == null)
            {
                return NotFound();
            }

            var fileModels = await db.FileModels
                .Where(fm => fm.ProductId == productsId.ProductId)
                .ToListAsync();

            return View(fileModels);
        }
       
        [Authorize(Roles = "Пользователь")]
        [HttpPost]
        public async Task<ActionResult> AddToCart(int? id, string choosedSize)
        {
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["ID"];
            
            var order = await db.Orders
    .Include(or => or.User)
    .Include(or => or.Employee)
    .Where(predicate => predicate.UserId.ToString() == cookieValue).Where(predicate=>predicate.EmployeeId==null)
    .Where(predicate=>predicate.DeliveryStatus==false).Where(predicate=>predicate.DeliveryAddress=="")
    .Select(or => or.Id)
    .FirstOrDefaultAsync();

            if (order != 0)
            {
                if (id != null)
                {
                    FileModel fileModel = await db.FileModels.FirstOrDefaultAsync(predicate => predicate.Id == id);
                    var cart1 = await db.Carts.Include(cart => cart.Order).Include(cart => cart.Product).Where(predicate => predicate.OrderId == order)
                        .Where(predicate => predicate.ProductId == fileModel.ProductId).Where(predicate=>predicate.Size == choosedSize).FirstOrDefaultAsync();
                    if (cart1 != null)
                    {
                        cart1.ProductAmount += 1;
                        db.Carts.Update(cart1);
                    }
                    else
                    {
                        if (choosedSize == null)
                        {
                            Cart cart = new Cart { OrderId = order, ProductId = fileModel.ProductId, ProductAmount = 1, Size = "S" };
                            db.Carts.Add(cart);
                        }
                        else
                        {
                            Cart cart = new Cart { OrderId = order, ProductId = fileModel.ProductId, ProductAmount = 1, Size = choosedSize };
                            db.Carts.Add(cart);
                        }
                    }
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            else
            {
                Order order1 = new Order { DeliveryAddress = "", DeliveryDate = DateTime.Now, DeliveryStatus = false, EmployeeId = null, SaleId = null, UserId = Convert.ToInt32(cookieValue) };
                db.Orders.Add(order1);
                await db.SaveChangesAsync();
                if (id != null)
                {
                    FileModel fileModel = db.FileModels.FirstOrDefault(predicate => predicate.Id == id);
                    if (choosedSize == null)
                    {
                        Cart cart = new Cart { OrderId = order1.Id, ProductId = fileModel.ProductId, ProductAmount = 1, Size = "S" };
                        db.Carts.Add(cart);
                    }
                    else
                    {
                        Cart cart = new Cart { OrderId = order1.Id, ProductId = fileModel.ProductId, ProductAmount = 1, Size = choosedSize };
                        db.Carts.Add(cart);
                    }
                  
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
           
            return NotFound();

        }

        public IActionResult Index(int? page, string query, string gender, string category)
        {
            var productIds = db.FileModels.Select(f => f.ProductId).Distinct().ToList();
            var fileModels = new List<FileModel>();

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            if (query == null && gender == null && category == null)
            {
                foreach (var productId in productIds)
                {
                    var fileModel = db.FileModels
                        .Include(f => f.Product)
                        .FirstOrDefault(f => f.ProductId == productId);
                    if (fileModel != null)
                    {
                      
                        fileModels.Add(fileModel);
                    }
                }
                return View(fileModels.ToPagedList(pageNumber, pageSize));
            }
            else if(gender=="Мужское" || gender==null)
            {
                if(category==null && query == null)
                    foreach (var productId in productIds)
                    {
                        var fileModel = db.FileModels
                            .Include(f => f.Product).Where(f=>f.Product.Gender==true)
                            .FirstOrDefault(f => f.ProductId == productId);
                        if (fileModel != null)
                        {
                            fileModels.Add(fileModel);
                        }
                    }
                else if (query != null) {
                    var results = db.FileModels.Include(f => f.Product).Where(i => i.Product.ProductName.Contains(query) || i.Product.Description.Contains(query)).Where(f => f.Product.Gender == true)
                        .Select(f => f.ProductId).Distinct().ToList();

                    foreach (var products in results)
                    {
                        var fileModel = db.FileModels
                            .Include(f => f.Product)
                            .FirstOrDefault(f => f.ProductId == products);
                        if (fileModel != null)
                        {
                            fileModels.Add(fileModel);
                        }
                    }
                }
                
                else
                {
                    foreach (var productId in productIds)
                    {
                        var fileModel = db.FileModels
                            .Include(f => f.Product).Where(f => f.Product.Gender == true).Where(f=>f.Product.ProductType.ProductTypeName == category)
                            .FirstOrDefault(f => f.ProductId == productId);
                        if (fileModel != null)
                        {
                            fileModels.Add(fileModel);
                        }
                    }
                }

                ViewBag.Gender = "Мужское";
                return View(fileModels.ToPagedList(pageNumber, pageSize));
            }
            else if (gender == "Женское")
            {
                if (category == null && query == null)
                    foreach (var productId in productIds)
                    {
                        var fileModel = db.FileModels
                            .Include(f => f.Product).Where(f => f.Product.Gender == false)
                            .FirstOrDefault(f => f.ProductId == productId);
                        if (fileModel != null)
                        {
                            fileModels.Add(fileModel);
                        }
                        
                    }
                else if (query != null)
                {
                    var results = db.FileModels.Include(f => f.Product).Where(i => i.Product.ProductName.Contains(query) || i.Product.Description.Contains(query)).Where(f => f.Product.Gender == true)
                        .Select(f => f.ProductId).Distinct().ToList();

                    foreach (var products in results)
                    {
                        var fileModel = db.FileModels
                            .Include(f => f.Product)
                            .FirstOrDefault(f => f.ProductId == products);
                        if (fileModel != null)
                        {
                            fileModels.Add(fileModel);
                        }
                    }
                }
                else
                {
                    foreach (var productId in productIds)
                    {
                        var fileModel = db.FileModels
                            .Include(f => f.Product).Where(f => f.Product.Gender == false).Where(f => f.Product.ProductType.ProductTypeName == category)
                            .FirstOrDefault(f => f.ProductId == productId);
                        if (fileModel != null)
                        {
                            fileModels.Add(fileModel);
                        }

                    }
                }
                ViewBag.Gender = "Женское";
                return View(fileModels.ToPagedList(pageNumber, pageSize));
            }

            
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFileCollection uploadedFile)
        {

            foreach (var file in uploadedFile)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                FileModel fileModel = new FileModel { Name = fileName, Path = "/Files/" + fileName };
                db.FileModels.Add(fileModel);
            }


            // путь к папке Files

            // сохраняем файл в папку Files в каталоге wwwroot

            await db.SaveChangesAsync();

            return RedirectToAction("FileModelIndex");
        }
       public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                var fileName =  Guid.NewGuid().ToString() + Path.GetExtension(uploadedFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", fileName);            
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = fileName, Path = "/Files/"+fileName };
                db.FileModels.Add(file);
               await db.SaveChangesAsync();
            }
            return RedirectToAction("FileModelIndex");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteFile(int? id)
        {
            if (id != null)
            {
                FileModel file = await db.FileModels.FirstOrDefaultAsync(predicate => predicate.Id == id);
                string path = _appEnvironment.WebRootPath  +"/Files/" + Path.GetFileName(file.Name);
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                    fileInfo.Delete();
                if (file != null)
                {
                    db.FileModels.Remove(file);
                    await db.SaveChangesAsync();
                    return RedirectToAction("FileModelIndex");
                }
            }
            return NotFound();
        }
    }
}
