using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scheduleBackend.Models.Database
{
    public class ApplicationUser : IdentityUser
    {
        
        public IEnumerable<SchedueleTable> schedueleTables { get; set; }
    }

    public class Base
    {
        [Key]
        public string Key { get; set; }

        [Timestamp]
        public byte[] Rowversion { get; set; }
    }

    public class SchedueleTable : Base
    {
        public string TaskName { get; set; }
        public string date { get; set; }
        public bool IsFree { get; set; }
        public string Location { get; set; }

        public string StartTime { get; set; } 

        public string EndTime { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User{ get; set; }
    }
}
