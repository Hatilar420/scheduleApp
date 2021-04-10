using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace scheduleBackend.Models.ClientResponse
{
    public class CreateTask
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string date { get; set; }
        [Required]
        public bool IsFree { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
    }
}
