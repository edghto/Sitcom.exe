using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitcoms.Core.Models;

namespace Sitcoms.Core
{
    public class SitcomsManager
    {
        private ICollection<Sitcom> sitcoms = new List<Sitcom>();
        private IUnitOfWork _UnitOfWork;

        public SitcomsManager(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public void Add(string name, int season, string sourceFile)
        {
            var parser = new EpisodeParsers.IMDBEpisodeParser(sourceFile);
            var episodes = parser.Episodes;

            var sitcom = _UnitOfWork.SitcomRepository.Find(s => s.Name == name).SingleOrDefault();
            if(sitcom == null)
            {
                sitcom = new Sitcom() { Name = name };
                _UnitOfWork.SitcomRepository.Add(sitcom);
            }

            sitcom.Episodes.Clear();
            foreach (var episode in episodes)
            {
                sitcom.Episodes.Add(episode);
            }

            _UnitOfWork.Complete();
        }

        public IEnumerable<SitcomWithSeasons> List()
        {
            return _UnitOfWork.SitcomRepository.GetSitcomsWithSeasons();
        }

        public IEnumerable<Season> Report(params ReportRequest[] requests)
        {
            var episodes = _UnitOfWork.EpisodeRepository.GetEpisodesByRequest(requests);

            return episodes;
        }

        public void SetLast(string name, int season, int last)
        {
            var episodes = _UnitOfWork.EpisodeRepository.GetEpisodesOfSeason(seasonNumber: season, sitcomName: name);
            if (episodes == null)
                throw new ArgumentException(string.Format("Season {0} for sitcom {1} doesn't exists", season, name));

            foreach (var episode in episodes.Where(e => e.Number <= last && !e.Watched))
            {
                episode.Watched = true;
            }

            _UnitOfWork.Complete();
        }
    }
}
