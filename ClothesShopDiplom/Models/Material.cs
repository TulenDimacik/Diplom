using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Display(Name = "Название материала")]
        public string MaterialName { get; set; }

    }
}
