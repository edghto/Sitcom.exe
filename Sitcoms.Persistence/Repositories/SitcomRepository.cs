using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitcoms.Core;
using Sitcoms.Core.Models;

namespace Sitcoms.Persistence.Repositories
{
    public class SitcomRepository
        : Repository<Core.Models.Sitcom>
        , Core.Repositories.ISitcomRepository
    {
        public SitcomRepository(SitcomsContext context) 
            : base(context)
        {
        }

        public IEnumerable<SitcomWithSeasons> GetSitcomsWithSeasons()
        {
            return SitcomsContext.Sitcoms
                .GroupJoin(SitcomsContext.Episodes,
                    s => s.Id,
                    e => e.Sitcom.Id,
                    (sitcom, episodes) => new SitcomWithSeasons
                    {
                        Name = sitcom.Name,
                        Seasons = episodes.Select(e => e.Season).Distinct()
                    })
                .ToList();
        }
        
        protected SitcomsContext SitcomsContext
        {
            get
            {
                return base.Context as SitcomsContext;
            }
        }
    }
}
