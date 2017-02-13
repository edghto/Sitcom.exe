using Sitcoms.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sitcoms.Persistence.EntityConfigurations
{
    class SitcomConfiguration : EntityTypeConfiguration<Sitcom>
    {
        public SitcomConfiguration()
        {
            Property(s => s.Name)
                .IsRequired();
        }
    }
}
