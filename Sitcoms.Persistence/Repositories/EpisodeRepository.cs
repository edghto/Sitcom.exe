using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Persistence.Repositories
{
    public class EpisodeRepository
        : Repository<Core.Models.Episode>
        , Core.Repositories.IEpisodeRepository
    {
        public EpisodeRepository(SitcomsContext context) 
            : base(context)
        {
        }
    }
}
