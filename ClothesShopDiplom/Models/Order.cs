using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesShopDiplom.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста адрес")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Адрес доставки должен быть от 5 до 100 символов")]
        [Display(Name = "Адрес доставки")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Выберите дату доставки")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата доставки")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "Статус доставки")]
        public bool DeliveryStatus { get; set; }

        [ForeignKey("UserId")]
        public int? UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("SaleId")]
        public int? SaleId { get; set; }

        public Sales Sales { get; set; }

        [ForeignKey("EmployeeId")]
        public int? EmployeeId { get; set; }

        public Employee Employee { get; set; }


    }
}
