using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Фамилию пожалуйста")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 25 символов")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите Имя пожалуйста")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 25 символов")]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста Логин")]
        [RegularExpression("[a-zA-z0-9]{3,}", ErrorMessage = "Логин должен состоять из латиницы или цифр и быть длиннее 3 символов ")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста Пароль")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Требования к паролю:\n cтрочная и заглавная латиница,\n цифра,\n спецсимвол(#?!@$%^&*-),\n длинна больше 7 cимволов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}

