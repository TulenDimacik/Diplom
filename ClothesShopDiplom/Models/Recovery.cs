using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Recovery
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public User User { get; set; }
        public Employee Employee { get; set; }
    }
}
