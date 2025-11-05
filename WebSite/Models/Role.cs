using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Role
    {

        [Key]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Название роли обязательно")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; } //список, пользоватей роли
    }
}
