using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class ProductSize
    {
        public int Id { get; set; }

        [ForeignKey("SizeId")]
        public int? SizeId { get; set; }

        public Size Size { get; set; }

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }

        public Product Product { get; set; }

    }
}
