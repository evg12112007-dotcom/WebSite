using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class Level
    {
        [Key]
        public int LevelID { get; set; }

        [Required(ErrorMessage ="Название уровня обязательно")]
        public string LevelName { get; set; }

    }
}
