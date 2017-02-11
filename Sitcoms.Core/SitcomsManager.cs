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

        public void Add(string name, int season, string sourceFile)
        {
            var parser = new EpisodeParsers.IMDBEpisodeParser(sourceFile);
            var episodes = parser.Episodes;

            var sitcom = sitcoms.Where(s => s.Name == name).SingleOrDefault();
            if (sitcom == null)
            {
                sitcom = new Sitcom() { Name = name };
                sitcoms.Add(sitcom);
            }

            var targetSeason = sitcom.Seasons.Where(s => s.Number == season).SingleOrDefault();
            if (targetSeason == null)
            {
                targetSeason = new Season() { Number = season };
                sitcom.Seasons.Add(targetSeason);
            }

            foreach (var episode in episodes)
            {
                targetSeason.Episodes.Add(episode);
            }
        }

        public ICollection<Sitcom> List()
        {
            return sitcoms;
        }

        public ICollection<Sitcom> Report(params ReportRequest[] requests)
        {
            if (requests.Any(r => r.Season == null))
                throw new NotImplementedException();

            var sitcomNames = requests.Select(r => r.Name);
            var list = sitcoms.Where(s => sitcomNames.Contains(s.Name));
            //TODO limit selection only to requested season

            return list.ToList();
        }

        public void SetLast(string name, int season, int last)
        {
            var targetSeason = sitcoms
                .Where(s => s.Name == name)
                .SelectMany(s => s.Seasons)
                .Where(s => s.Number == season)
                .SingleOrDefault();

            if (targetSeason == null)
                throw new ArgumentException(string.Format("Season {0} for sitcom {1} doesn't exists", season, name));

            targetSeason.Last = last;
        }
    }
}
