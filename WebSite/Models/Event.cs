using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Event
    {
        [Key]
        public Guid EventID { get; set; }

        [Required(ErrorMessage = "Название события обязательно")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Дата начала обязательна")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Дата конца обязательна")]
        public DateTime EndDate { get; set; }

        public ICollection<Tournament> Tournaments { get; set; } //из события получить список турниров
    }
}
