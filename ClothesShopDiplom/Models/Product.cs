using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Product
    {      
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Название товара")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "Длина названия должна быть от 5 до 40 символов")]
        [Display(Name = "Название")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Введите Цвет ")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина цвета должна быть от 3 до 25 символов")]
        [Display(Name = "Цвет")]
        public string ProductColor { get; set; }
        
        [Required(ErrorMessage ="Введите Цену")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Введите корректную цену")]
        [Display(Name = "Цена")]
        public double Price { get; set; }

        [Display(Name = "Мужской")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Введите описание товара")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [ForeignKey("ProductTypeId")]
        [Display(Name = "Вид товара")]
        public int? ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        [ForeignKey("MaterialId")]
        [Display(Name = "Материал")]
        public int? MaterialId { get; set; }

        public Material Material { get; set; }


    }
  
}
