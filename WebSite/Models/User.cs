using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class User:IdentityUser<Guid>
    {

        public string? SteamID64 { get; set; }

        public string? RiotPUUID { get; set; }

        public string? Name { get; set; }

        [Range(0, 120, ErrorMessage = "Возраст должен быть корректным числом")]
        public int Age { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
