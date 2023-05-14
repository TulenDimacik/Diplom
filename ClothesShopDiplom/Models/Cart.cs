using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductAmount { get; set; }
        public string Size { get; set; }

        [ForeignKey("OrderId")]
        public int? OrderId { get; set; }

        public Order Order { get; set; }

        [ForeignKey("ProductId")]
        public int? ProductId { get; set; }

        public Product Product { get; set; }

    }
}
