using System.ComponentModel.DataAnnotations;

namespace WebSite.ViweModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите почту")]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
