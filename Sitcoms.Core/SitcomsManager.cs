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
        private IEpisodeParser _EpisodeParser;

        public SitcomsManager(IUnitOfWork unitOfWork)
            : this(unitOfWork, new EpisodeParsers.IMDBEpisodeParser())
        { }

        public SitcomsManager(IUnitOfWork unitOfWork, IEpisodeParser episodeParser)
        {
            _UnitOfWork = unitOfWork;
            _EpisodeParser = episodeParser;
        }

        public void Add(string name, int? season, string sourceFile)
        {
            var parser = new EpisodeParsers.IMDBEpisodeParser();
            var episodes = _EpisodeParser.Parse(sourceFile);

            var sitcom = _UnitOfWork.SitcomRepository.Find(s => s.Name == name).SingleOrDefault();
            if(sitcom == null)
            {
                sitcom = new Sitcom() { Name = name };
                _UnitOfWork.SitcomRepository.Add(sitcom);
            }

            foreach (var episode in episodes)
            {
                episode.Season = season ?? episode.Season;
                sitcom.Episodes.Add(episode);
            }
            _UnitOfWork.Complete();
        }

        public void Delete(string name, int? season = null)
        {
            if (season == null)
            {
                var sitcom = _UnitOfWork.SitcomRepository.Find(s => s.Name == name).SingleOrDefault();
                if (sitcom == null)
                {
                    var msg = string.Format("Sitcom {0} doesn't exist", name);
                    throw new Repositories.SitcomNotFoundException(msg);
                }
                _UnitOfWork.SitcomRepository.Remove(sitcom);
            }
            else
            {
                var request = new ReportRequest() { Name = name, Season = (int)season };
                var targetSeason = _UnitOfWork.EpisodeRepository.GetEpisodesByRequest(request).Single();
                _UnitOfWork.EpisodeRepository.RemoveRange(targetSeason.Episodes);
            }

            _UnitOfWork.Complete();
        }

        public IEnumerable<SitcomWithSeasons> List()
        {
            return _UnitOfWork.SitcomRepository.GetSitcomsWithSeasons();
        }

        public IEnumerable<Season> Report(params ReportRequest[] requests)
        {
            if(requests.Count() == 0)
            {
                requests = _UnitOfWork.SitcomRepository
                    .GetAll()
                    .Select(s => new ReportRequest() { Name = s.Name })
                    .ToArray();
            }

            var episodes = _UnitOfWork.EpisodeRepository.GetEpisodesByRequest(requests);

            return episodes;
        }

        public void SetLast(string name, int season, int last)
        {
            var episodes = _UnitOfWork.EpisodeRepository.GetEpisodesOfSeason(seasonNumber: season, sitcomName: name);
            if (episodes == null || episodes.Count() == 0)
            {
                var msg = string.Format("Season {0} for sitcom {1} doesn't exist", season, name);
                throw new Repositories.SeasonNotFoundException(msg);
            }

            if(episodes.Select(e => e.Number).Max() < last)
            {
                var msg = string.Format("S{0:00}E{1:00} for sitcom {2} doesn't exist", season, last, name);
                throw new Repositories.EpisodeNotFoundException(msg);
            }

            foreach (var episode in episodes)
            {
                episode.Watched = episode.Number <= last;
            }
            _UnitOfWork.Complete();
        }
    }
}
