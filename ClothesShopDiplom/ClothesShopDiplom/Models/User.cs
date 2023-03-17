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
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите Имя пожалуйста")]
        public string Name { get; set; }
        public string Patronymic { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        // [Required(ErrorMessage = "Введите пожалуйста Телефон")]
        //  public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста Пароль")]
        public string Password { get; set; }
    }
}
