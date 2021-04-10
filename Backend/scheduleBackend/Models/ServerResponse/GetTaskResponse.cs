using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scheduleBackend.Models.ServerResponse
{
    public class GetTaskResponse
    {
        public string TaskName { get; set; }
        public string date { get; set; }
        public bool IsFree { get; set; }
        public string Location { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
