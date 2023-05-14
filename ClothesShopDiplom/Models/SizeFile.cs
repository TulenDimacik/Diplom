using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class SizeFile
    {
        public int Id { get; set; }

        [ForeignKey("ProductSizeId")]
        public int? ProductSizeId { get; set; }

        public ProductSize ProductSize { get; set; }

        [ForeignKey("FileModelId")]
        public int? FileModelId { get; set; }

        public FileModel FileModel { get; set; }
    }
}
