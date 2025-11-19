using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Match
    {
        [Key]
        public Guid MatchID { get; set; }

        [Required]
        public Guid TournamentID { get; set; }
        [ForeignKey("TournamentID")]
        public Tournament Tournament { get; set; } //информация о турнире

        [Required]
        public Guid TeamAID { get; set; }
        [ForeignKey("TeamAID")]
        public Team TeamA { get; set; } //информация о команде

        [Required]
        public Guid TeamBID { get; set; }
        [ForeignKey("TeamBID")]
        public Team TeamB { get; set; } //информация о команде

        public Guid? WinnerTeamID { get; set; }
        [ForeignKey("WinnerTeamID")]
        public Team WinnerTeam { get; set; } //информация о команде

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Счёт должен быть неотрицательным")]
        public int Score { get; set; }

        public ICollection<Referee> Referees { get; set; } //судья, обслуживающий этот матч

    }
}
