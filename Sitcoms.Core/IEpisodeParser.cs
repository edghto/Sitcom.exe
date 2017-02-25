using Sitcoms.Core.Models;
using System.Collections.Generic;

namespace Sitcoms.Core
{
    public interface IEpisodeParser
    {
        ICollection<Episode> Parse(string fileName);
    }
}
