using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "Никнейм обязательный")]
        public string NickName { get; set; }

        public int CS2SkillLevel { get; set; }

        public int Dota2SkillLevel { get; set; }

        public int ValorantSkillLevel { get; set; }

        [Required]
        public int TeamID { get; set; }
        [ForeignKey("TeamID")]
        public Team Team { get; set; } //получить команду, в которой играет игрок

        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        public int LevelID { get; set; }
        [ForeignKey("LevelID")]
        public Level Level { get; set; }


    }
}
