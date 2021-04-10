using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scheduleBackend.Models.Database
{
    public class SchdueleContext  : IdentityDbContext<ApplicationUser>
    {
        public SchdueleContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SchedueleTable> schedueles { get; set; }
    }
}
