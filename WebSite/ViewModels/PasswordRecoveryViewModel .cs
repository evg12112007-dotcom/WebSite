using System.ComponentModel.DataAnnotations;

namespace WebSite.ViewModels
{
    public class PasswordRecoveryViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Введите корректный Email")]
        public string Email { get; set; }
    }

}
