using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Persistence.Repositories
{
    public class SeasonRepository
        : Repository<Core.Models.Season>
        , Core.Repositories.ISeasonRepository
    {
        public SeasonRepository(SitcomsContext context) 
            : base(context)
        {
        }
    }
}
