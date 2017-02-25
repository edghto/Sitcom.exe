using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sitcoms.Core;
using Sitcoms.Core.Models;
using Sitcoms.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Tests
{
    using PredicateType = Expression<Func<Sitcom, bool>>;

    [TestClass]
    public class SitcomManagerDeleteTests
    {
        private SitcomsManager _SitcomManager;
        private Mock<IUnitOfWork> _UnitOfWork;
        private Mock<ISitcomRepository> _SitcomRepository;
        private Mock<IEpisodeRepository> _EpisodeRepository;
        protected IList<Episode> _Episodes;

        protected IList<Sitcom> _Data = new List<Sitcom>()
        {
            new Sitcom() {
                Name = "foo",
                Episodes = new List<Episode>() {
                    new Episode() { Season = 1 },
                    new Episode() { Season = 1 },
                    new Episode() { Season = 2 },
                    new Episode() { Season = 2 },
                    new Episode() { Season = 3 },
                    new Episode() { Season = 3 },
                }
            },
            new Sitcom() {
                Name = "boo",
                Episodes = new List<Episode>() {
                    new Episode() { Season = 1 },
                    new Episode() { Season = 1 },
                    new Episode() { Season = 2 },
                    new Episode() { Season = 2 },
                    new Episode() { Season = 3 },
                    new Episode() { Season = 3 },
                }
            },
            new Sitcom() {
                Name = "bar",
                Episodes = new List<Episode>() {
                    new Episode() { Season = 1 },
                    new Episode() { Season = 1 },
                    new Episode() { Season = 2 },
                    new Episode() { Season = 2 },
                    new Episode() { Season = 3 },
                    new Episode() { Season = 3 },
                }
            },
        };

        [TestInitialize()]
        public void Initialize()
        {
            _SitcomRepository = new Mock<ISitcomRepository>(MockBehavior.Strict);
            _EpisodeRepository = new Mock<IEpisodeRepository>(MockBehavior.Strict);
            _UnitOfWork = new Mock<IUnitOfWork>();
            _SitcomManager = new SitcomsManager(_UnitOfWork.Object);

            _UnitOfWork.Setup(s => s.SitcomRepository).Returns(_SitcomRepository.Object);
            _UnitOfWork.Setup(s => s.EpisodeRepository).Returns(_EpisodeRepository.Object);
        }

        [TestMethod]
        public void DeleteExistingSitcom()
        {
            string name = "foo";
            var fooSitcom = _Data.Where(s => s.Name == name);

            _SitcomRepository.Setup(s => s.Find(It.IsAny<PredicateType>())).Returns(fooSitcom);
            _SitcomRepository.Setup(s => s.Remove(fooSitcom.Single()));
            _SitcomManager.Delete(name: name);

            _SitcomRepository.Verify(s => s.Remove(fooSitcom.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(SitcomNotFoundException))]
        public void DeleteNonExistingSitcom()
        {
            string name = "null";
            var fooSitcom = _Data.Where(s => s.Name == name);

            _SitcomRepository.Setup(s => s.Find(It.IsAny<PredicateType>())).Returns(new List<Sitcom>() { });
            _SitcomManager.Delete(name: name);
        }

        [TestMethod]
        public void DeleteExistingSeason()
        {
            string name = "foo";
            int seasonNumber = 2;
            var episodes = _Data
                .Where(s => s.Name == name)
                .SelectMany(s => s.Episodes)
                .Where(e => e.Season == seasonNumber);
            var seasons = new List<Season>()
            {
                new Season() { Episodes = episodes }
            };

            _EpisodeRepository
                .Setup(e => e.GetEpisodesByRequest(It.IsAny<ReportRequest[]>())) // TODO: check that correct request is send
                .Returns(seasons);
            _EpisodeRepository.Setup(e => e.RemoveRange(episodes));
            
            _SitcomManager.Delete(name: name, season: seasonNumber);
            _EpisodeRepository.Verify(e => e.RemoveRange(episodes));
        }
    }
}
