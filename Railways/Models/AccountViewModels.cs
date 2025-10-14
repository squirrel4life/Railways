using System.ComponentModel.DataAnnotations;

namespace Railways.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        
        [Display(Name = "Країна")]
        public string Country { get; set; }
        [Display(Name = "Місто")]
        public string HomeTown { get; set; }
        [Display(Name = "Вік")]
        [Required]
        [Range(0, 200, ErrorMessage = "Вік не може бути від'ємним або занадто великим!")]
        public int Age { get; set; }
        [Display(Name = "Телефон")]
        [Phone]
        public string Phone { get; set; }
        
        [Display(Name = "Кредитна карта")]
        [CreditCard]
        public string CreditCard { get; set; }
        [Display(Name = "Статус")]
        [Required]
        public string SpecialInfo { get; set; }
        [Display(Name = "Інформація про себе")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} має містити не менш ніж {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новий пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значення {0} має містити не менш ніж {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
       
        [Display(Name = "Країна")]
        public string Country { get; set; }
        [Display(Name = "Місто")]
        public string HomeTown { get; set; }
        [Display(Name = "Вік")]
        [Required]
        [Range(0, 200, ErrorMessage = "Вік не може бути від'ємним або занадто великим!")]        
        public int Age { get; set; }
        [Display(Name = "Телефон")]
        [Phone]
        public string Phone { get; set; }
        
        [Display(Name = "Кредитна карта")]
        [CreditCard]
        public string CreditCard { get; set; }
        [Display(Name = "Статус")]
        [Required]
        public string SpecialInfo { get; set; }
        [Display(Name = "Інформація про себе")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }

    public class EditProfileViewModel
    {       

        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Display(Name = "Країна")]
        public string Country { get; set; }
        [Display(Name = "Місто")]
        public string HomeTown { get; set; }
        [Display(Name = "Вік")]
        [Required]
        [Range(0, 200, ErrorMessage = "Вік не може бути від'ємним або занадто великим!")]
        public int Age { get; set; }
        [Display(Name = "Телефон")]
        [Phone]
        public string Phone { get; set; }

        [Display(Name = "Кредитна карта")]
        [CreditCard]
        public string CreditCard { get; set; }
        [Display(Name = "Статус")]
        [Required]
        public string SpecialInfo { get; set; }
        [Display(Name = "Інформація про себе")]
        [DataType(DataType.MultilineText)]
        public string AboutMe { get; set; }
    }
}
