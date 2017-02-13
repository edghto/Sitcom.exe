using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Persistence
{
    public class SitcomsContext : DbContext
    {
        public DbSet<Core.Models.Sitcom> Sitcoms { get; set; }
        public DbSet<Core.Models.Episode> Episodes { get; set; }
    }
}
