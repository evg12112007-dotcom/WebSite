using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string SteamID64 { get; set; }

        public string RiotPUUID { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательно")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        public string PasswordHash { get; set; }

        [Required]
        [Range(0, 120, ErrorMessage = "Возраст должен быть корректным числом")]
        public int Age { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

    }
}
