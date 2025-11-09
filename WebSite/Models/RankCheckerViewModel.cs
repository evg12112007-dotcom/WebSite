using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class RankCheckerViewModel
    {
        [Required(ErrorMessage = "Это обязательное поле")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "Неполный SteamID")]
        public string SteamID { get; set; }
    }
}
