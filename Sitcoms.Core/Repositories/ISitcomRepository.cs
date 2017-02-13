using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitcoms.Core.Models;

namespace Sitcoms.Core.Repositories
{
    public interface ISitcomRepository : IRepository<Sitcom>
    {
        IEnumerable<SitcomWithSeasons> GetSitcomsWithSeasons();
    }
}
