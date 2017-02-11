using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Core
{
    public interface IEpisodeParser
    {
        ICollection<Episode> Episodes { get; }
    }
}
