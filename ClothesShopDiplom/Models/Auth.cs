using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Auth
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public User User{ get; set; }
        public Employee Employee { get; set; }
    }
}
