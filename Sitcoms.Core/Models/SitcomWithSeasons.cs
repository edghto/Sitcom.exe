using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Core.Models
{
    public class SitcomWithSeasons
    {
        public string Name { get; set; }
        public IEnumerable<int> Seasons { get; set; }

        public SitcomWithSeasons()
        {
            Seasons = new List<int>();
        }
    }
}
