using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Tournament
    {
        [Key]
        public int TournamentID { get; set; }

        [Required(ErrorMessage = "Название турнира обязательно")]
        public string TournamentName { get; set; }

        [Required]
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; } 

        [Required]
        public int GameId { get; set; }
        [ForeignKey("GameId")]
        public Game Game { get; set; } 


        [Required(ErrorMessage = "Дата начала обязательна")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Дата окончания обязательна")]
        public DateTime EndDate { get; set; }


        public ICollection<Team> Teams { get; set; } // список команд турнира
        public ICollection<Match> Matches { get; set; } //список матчей, относящихся к турниру
        public ICollection<TournamentStanding> TournamentStandings { get; set; } //результаты турнира


    }
}
