using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels
{
    public class PasswordResetViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
