using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class EmailController: Controller
    {

        private readonly UserManager<User> _userManager;

        public EmailController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return View("Error"); // если что-то не передано
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Error"); // пользователь не найден
            }

            //подтверждаем email
            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View("ConfirmEmailSuccess"); // успешная подтверждённая почта
            }

            return View("Error"); // ошибка при подтверждении
        }

        public IActionResult ConfirmEmailSuccess()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    
}
}
