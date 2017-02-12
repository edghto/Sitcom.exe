using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
