using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models 
{ 
    public class Referee
    {
        [Key]
        public int RefereeID { get; set; }

        [Required(ErrorMessage = "Имя судьи обязательно")]
        public string RefereeName { get; set; }

        public ICollection<Match> Match { get; set; } //список матчей судьи
    }
}
