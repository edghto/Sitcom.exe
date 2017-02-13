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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            modelBuilder.Configurations.Add(new EntityConfigurations.EpisodeConfiguration());
            modelBuilder.Configurations.Add(new EntityConfigurations.SitcomConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
