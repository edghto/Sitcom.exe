using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Core.Models
{
    public class Season
    {
        public Sitcom Sitcom { get; set; }
        public int Number { get; set; }
        public IEnumerable<Episode> Episodes { get; set; }
    }
}
