using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class TournamentStanding
    {
        [Key]
        public int StandingsId { get; set; }

        [Required]
        public int TournamentID { get; set; }
        [ForeignKey("TournamentID")]
        public Tournament Tournament { get; set; } 

        [Required]
        public int TeamID { get; set; }
        [ForeignKey("TeamID")]
        public Team Team { get; set; } 

        [Range(0, int.MaxValue, ErrorMessage = "Количество сыгранных матчей не может быть отрицательным")]
        public int MatchesPlayed { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество побед не может быть отрицательным")]
        public int Wins { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество поражений не может быть отрицательным")]
        public int Losses { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество ничьих не может быть отрицательным")]
        public int Draws { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Очки не могут быть отрицательными")]
        public int Points { get; set; }
    }
}
