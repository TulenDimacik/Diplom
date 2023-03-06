using ClothesShopDiplom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Controllers
{
    public class HomeController : Controller
    {
        //хуйня для бд
        private ApplicationContext db;


        public HomeController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        #region  Сотрудник
        //получение данных из бд
        public async Task<IActionResult> Index2()
        {
            return View(await db.Employees.ToListAsync());
        }
        //Ретурн Вьюшки
        public IActionResult Create()
        {
            return View();
        }
        //Добавление данных получается
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index2");
        }
        #endregion
        #region Роль
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
            return RedirectToAction("Login");
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
