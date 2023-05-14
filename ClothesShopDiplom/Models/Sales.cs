using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Sales
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Текст промокода")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Длина промокода должна быть от 3 до 15 символов")]
        [Display(Name = "Промокод")]
        public string SaleText { get; set; }

        [Required(ErrorMessage = "Введите Размер скидки")]
        [Display(Name = "Размер скидки")]
        public int SaleAmount { get; set; }

        [Display(Name = "Статус скидки")]
        public bool SaleStatus { get; set; }
    }
}
