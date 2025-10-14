using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Railways
{
    public class SearchPath
    {
        [Display(Name = "Звідки")]
        [Required(ErrorMessage = "Обов'язково заповніть це поле!")]
        public string Start { get; set; }
        [Display(Name = "Куди")]
        [Required(ErrorMessage = "Обов'язково заповніть це поле!")]
        public string End { get; set; }
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Вкажіть дату!")]
        [CheckDate]
        public DateTime DateOfTrip { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDateAttribute: ValidationAttribute
    {       
        public override string FormatErrorMessage(string name)
        {
            return "Замовляти квитки можна не раніше, ніж за 45 днів до відправлення!";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            DateTime check = (DateTime)value;
            DateTime today = DateTime.Now;
            DateTime offset = today.AddDays(45);
            if (check >= today && check <= offset)
                return true;
            return false;
        }
    }
}