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

        /// <summary>
        /// Each request contains sitcom name and optional season number.
        /// If season number is skipped, than last season of requested sitcom is choosed
        /// </summary>
        /// <param name="requests">Array of requests</param>
        /// <exception cref="SitcomNotFoundException">One of requested sitcom doesn't exists</exception>
        /// <exception cref="SeasonNotFoundException">One of requested season of sitcom doesn't exists</exception>
        /// <returns>Collection of Seasons</returns>
        IEnumerable<Season> GetEpisodesByRequest(ReportRequest[] requests);
    }
}
