using Sitcoms.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Sitcoms.Persistence.EntityConfigurations
{
    class EpisodeConfiguration : EntityTypeConfiguration<Episode>
    {
        public EpisodeConfiguration()
        {
            Property(e => e.Name)
                .IsRequired();
        }
    }
}
