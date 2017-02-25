using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Sitcoms.Core;
using Sitcoms.Core.Repositories;
using Sitcoms.Core.Models;
using Moq;

namespace Sitcoms.Tests
{
    [TestClass]
    public class SitcomsManagerListTests
    {
        private SitcomsManager _SitcomManager;
        private Mock<IUnitOfWork> _UnitOfWork;
        private Mock<IEpisodeRepository> _EpisodeRepository;
        private Mock<ISitcomRepository> _SitcomRepository;

        [TestInitialize()]
        public void Initialize()
        {
            _EpisodeRepository = new Mock<IEpisodeRepository>(MockBehavior.Strict);
            _SitcomRepository = new Mock<ISitcomRepository>(MockBehavior.Strict);
            _UnitOfWork = new Mock<IUnitOfWork>();
            _SitcomManager = new SitcomsManager(_UnitOfWork.Object);

            _UnitOfWork.Setup(s => s.EpisodeRepository).Returns(_EpisodeRepository.Object);
            _UnitOfWork.Setup(s => s.SitcomRepository).Returns(_SitcomRepository.Object);
        }

        [TestCleanup()]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void List_IsEmptyTest()
        {
            _SitcomRepository.Setup(s => s.GetSitcomsWithSeasons()).Returns(new List<SitcomWithSeasons>());

            var actual = _SitcomManager.List();
            _UnitOfWork.VerifyGet(s => s.SitcomRepository);
            _SitcomRepository.Verify(s => s.GetSitcomsWithSeasons());

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void List_IsNotEmptyTest()
        {
            var seasons = new List<SitcomWithSeasons>()
            {
                new SitcomWithSeasons() { Name = "foo", Seasons = new List<int>() { 1, 2 ,3 } }
            };
            _SitcomRepository.Setup(s => s.GetSitcomsWithSeasons()).Returns(seasons);

            var actual = _SitcomManager.List();
            _UnitOfWork.VerifyGet(s => s.SitcomRepository);
            _SitcomRepository.Verify(s => s.GetSitcomsWithSeasons());

            Assert.AreEqual(1, actual.Count());
            Assert.AreEqual(3, actual.First().Seasons.Count());
        }
    }
}
