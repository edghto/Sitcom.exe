using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sitcoms.Core;
using Sitcoms.Core.Models;
using Sitcoms.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Tests
{
    [TestClass]
    public class SitcomManagerSetLastTests
    {
        private SitcomsManager _SitcomManager;
        private Mock<IUnitOfWork> _UnitOfWork;
        private Mock<IEpisodeRepository> _EpisodeRepository;
        protected IList<Episode> _Episodes;

        protected readonly string _Name = "foo";
        protected readonly int _Season = 1;

        [TestInitialize()]
        public void Initialize()
        {
            _EpisodeRepository = new Mock<IEpisodeRepository>(MockBehavior.Strict);
            _UnitOfWork = new Mock<IUnitOfWork>();
            _SitcomManager = new SitcomsManager(_UnitOfWork.Object);

            _UnitOfWork.Setup(s => s.EpisodeRepository).Returns(_EpisodeRepository.Object);

            _Episodes = new List<Episode>()
            {
                new Episode() { Number = 1, Watched = false },
                new Episode() { Number = 2, Watched = false },
                new Episode() { Number = 3, Watched = false },
            };
            _EpisodeRepository
                .Setup(e => e.GetEpisodesOfSeason(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(_Episodes);
        }

        [TestMethod]
        [ExpectedException(typeof(EpisodeNotFoundException))]
        public void SetLast_EpisodeDoesNotExists()
        {
            int last = 5;

            _SitcomManager.SetLast(_Name, _Season, last);
            _EpisodeRepository.Verify(e => e.GetEpisodesOfSeason(_Season, _Name));

            Assert.IsFalse(_Episodes.Select(e => e.Watched).Any(w => w));
        }

        [TestMethod]
        public void SetLast_EpisodeDoExists()
        {
            int last = 1;

            _SitcomManager.SetLast(_Name, _Season, last);
            _EpisodeRepository.Verify(e => e.GetEpisodesOfSeason(_Season, _Name));

            Assert.IsTrue(_Episodes.Where(e => e.Number == 1).Select(e => e.Watched).Single());
        }

        [TestMethod]
        public void SetLast_UpdatesAllLowerEqualThanLast()
        {
            int last = 2;

            _SitcomManager.SetLast(_Name, _Season, last);
            _EpisodeRepository.Verify(e => e.GetEpisodesOfSeason(_Season, _Name));

            Assert.IsTrue(_Episodes.Where(e => e.Number <= 2).Select(e => e.Watched).All(w => w));
            Assert.IsFalse(_Episodes.Where(e => e.Number > 2).Select(e => e.Watched).Any(w => w));
        }

        [TestMethod]
        public void SetLast_SetAsUnwatchedAllGreaterThanLast()
        {
            int last = 2;

            foreach (var e in _Episodes)
                e.Watched = true;

            _SitcomManager.SetLast(_Name, _Season, last);
            _EpisodeRepository.Verify(e => e.GetEpisodesOfSeason(_Season, _Name));

            Assert.IsTrue(_Episodes.Where(e => e.Number <= 2).Select(e => e.Watched).All(w => w));
            Assert.IsFalse(_Episodes.Where(e => e.Number > 2).Select(e => e.Watched).Any(w => w));
        }
    }
}
