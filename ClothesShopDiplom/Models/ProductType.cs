using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Display(Name = "Название вида товара")]
        public string ProductTypeName { get; set; }

    }
}
