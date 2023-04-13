using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Size
    {
        public int Id { get; set; }

        [Display(Name = "Размер")]
        public string SizeName { get; set; }
    }
}
