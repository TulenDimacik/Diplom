using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FileModel> FileModels { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductType> ProductTypes  { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
         //   Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public void Seed(ApplicationContext context)
        {

            // добавление данных в модель
            if (context.Roles.Count() == 0)
            {
                context.Roles.Add(new Role { RoleName = "Администратор" });
                context.Roles.Add(new Role { RoleName = "Товаровед" });
                context.Roles.Add(new Role { RoleName = "Оператор" });
                context.SaveChanges();
                context.Employees.Add(new Employee { Surname = "Admin", Name = "Admin", Patronymic = "Admin", Email = "admin@mail.ru", Login = "Admin123", Password = BCrypt.Net.BCrypt.HashPassword("Dima2003$"), RoleId = 1 });
                context.SaveChanges();
            }
        }
        //protected override void Seed(ApplicationContext context)
        //{
        //    // добавление данных в модель
        //    context.Roles.Add(new Role { RoleName = "Администратор" });
        //    context.Roles.Add(new Role { RoleName = "Товаровед" });
        //    context.Roles.Add(new Role { RoleName = "Оператор" });
        //    context.SaveChanges();
        //    context.Employees.Add(new Employee {Surname = "Admin",Name = "Admin", Patronymic ="Admin", Email="admin@mail.ru", Login = "Admin123", RoleId = 1});
            
        //}
    }
}
