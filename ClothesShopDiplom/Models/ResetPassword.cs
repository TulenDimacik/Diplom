using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Введите пожалуйста Пароль")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Требования к паролю:\n cтрочная и заглавная латиница,\n цифра,\n спецсимвол(#?!@$%^&*-),\n длинна больше 7 cимволов")]
        public string NewPassword { get; set; }
        
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
        public User User { get; set; }
        public Employee Employee { get; set; }
    }
}
