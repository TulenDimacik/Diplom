using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class History
    {
        public int Id { get; set; }

       
        [ForeignKey("CartID")]
        public int? CartID{ get; set; }

        public Cart Cart { get; set; }

        [ForeignKey("FileModelId")]
        public int? FileModelId { get; set; }

        public FileModel FileModel { get; set; }


    }
}
