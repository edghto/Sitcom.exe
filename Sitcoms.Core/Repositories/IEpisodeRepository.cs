using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitcoms.Core.Models;

namespace Sitcoms.Core.Repositories
{
    public interface IEpisodeRepository : IRepository<Episode>
    {
        IEnumerable<Models.Episode> GetEpisodesOfSeason(int seasonNumber, string sitcomName);
        IEnumerable<Season> GetEpisodesByRequest(ReportRequest[] requests);
    }
}
