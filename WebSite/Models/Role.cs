using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class Role : IdentityRole<Guid>
    {

        public string Description { get; set; }

        public ICollection<User> Users { get; set; } //список, пользоватей роли
    }
}
