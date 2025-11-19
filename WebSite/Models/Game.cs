using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Game
    {
        [Key]
        public Guid GameId { get; set;}
        [Required(ErrorMessage = "Название игры обязательно")]
        public string GameName { get; set;}

        public ICollection<Tournament> Tournaments { get; set; } // получить все турниры по одной игре
    }
}
