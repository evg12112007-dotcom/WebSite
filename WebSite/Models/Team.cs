using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Team
    {
        [Key]
        public Guid TeamId { get; set; }

        [Required(ErrorMessage = "Название команды обязательно")]
        public string TeamName { get; set; }
        [Required(ErrorMessage = "Название организации обязательно")]
        public string Institution { get; set; }
        [Required(ErrorMessage = "Наличие капитана обязательно")]
        public string CaptainName { get; set; }
        public ICollection<Player> Players { get; set; } //список игроков в команде

        public ICollection<Tournament> Tournaments { get; set; } //список турниров команды

    }
}
