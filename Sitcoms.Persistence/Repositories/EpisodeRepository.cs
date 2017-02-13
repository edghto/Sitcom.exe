using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitcoms.Core;
using System.Data.Entity;
using Sitcoms.Core.Models;

namespace Sitcoms.Persistence.Repositories
{
    public class EpisodeRepository
        : Repository<Core.Models.Episode>
        , Core.Repositories.IEpisodeRepository
    {
        public EpisodeRepository(SitcomsContext context) 
            : base(context)
        {
        }
        
        public IEnumerable<Episode> GetEpisodesOfSeason(int seasonNumber, string sitcomName)
        {
            return SitcomsContext.Episodes
                .Include(e => e.Sitcom)
                .Where(e => e.Sitcom.Name == sitcomName && e.Season == seasonNumber)
                .ToList();
        }

        public IEnumerable<Season> GetEpisodesByRequest(ReportRequest[] requests)
        {
            var seasons = new List<Season>();
            foreach (var request in requests)
            {
                int seasonNumber = request.Season??GetLastSeason(request.Name);
                var episodes = GetEpisodesOfSeason(seasonNumber, request.Name);

                seasons.Add(new Season()
                {
                    Sitcom = episodes.First().Sitcom,
                    Number = episodes.First().Season,
                    Episodes = episodes
                });
            }

            return seasons;
        }

        private int GetLastSeason(string sitcomName)
        {
            return SitcomsContext.Episodes
                .Include(e => e.Sitcom)
                .Where(e => e.Sitcom.Name == sitcomName)
                .Select(e => e.Season)
                .Max();
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
