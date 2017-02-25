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
    public class SitcomManagerAddTests
    {
        private Mock<IEpisodeParser> _EpisodeParserMock;
        private SitcomsManager _SitcomManager;
        private Mock<ISitcomRepository> _SitcomRepository;
        private Mock<IUnitOfWork> _UnitOfWork;

        private IList<Episode> _Episodes = new List<Episode>()
        {
            new Episode() { Number = 1, Season = 1 },
            new Episode() { Number = 2, Season = 1 },
            new Episode() { Number = 3, Season = 1 },
            new Episode() { Number = 4, Season = 1 },
        };

        [TestInitialize()]
        public void Initialize()
        {
            _EpisodeParserMock = new Mock<IEpisodeParser>(MockBehavior.Strict);
            _SitcomRepository = new Mock<ISitcomRepository>();
            _UnitOfWork = new Mock<IUnitOfWork>();
            _SitcomManager = new SitcomsManager(_UnitOfWork.Object, _EpisodeParserMock.Object);

            _UnitOfWork.Setup(s => s.SitcomRepository).Returns(_SitcomRepository.Object);
        }

        [TestMethod]
        public void AddNewSitcom()
        {
            string name = "foo";
            string fileName = "fooFile";
            Sitcom sitcom = null;

            _EpisodeParserMock.Setup(e => e.Parse(It.Is<string>(s => s == fileName))).Returns(_Episodes);
            _SitcomRepository.Setup(r => r.Add(It.IsAny<Sitcom>())).Callback((Sitcom s) => { sitcom = s; });
            _SitcomManager.Add(name, null, fileName);

            _SitcomRepository.Verify(r => r.Add(It.Is<Sitcom>(s => s.Name == name)));
            Assert.IsNotNull(sitcom);
            Assert.AreEqual(0,_Episodes.Except(sitcom.Episodes).Count());
            Assert.AreEqual(0, sitcom.Episodes.Except(_Episodes).Count());
        }

        [TestMethod]
        public void AddNewSeasonToSitcom()
        {
            string name = "foo";
            string fileName = "fooFile";
            Sitcom sitcom = new Sitcom() { Name = name };

            _EpisodeParserMock.Setup(e => e.Parse(It.Is<string>(s => s == fileName))).Returns(_Episodes);
            _SitcomRepository.Setup(r => r.Find(It.IsAny<PredicateType>())).Returns(new List<Sitcom>() { sitcom });
            _SitcomManager.Add(name, null, fileName);

            _SitcomRepository.Verify(r => r.Add(It.Is<Sitcom>(s => s.Name == name)), Times.Never);
            Assert.AreEqual(0, _Episodes.Except(sitcom.Episodes).Count());
            Assert.AreEqual(0, sitcom.Episodes.Except(_Episodes).Count());
            Assert.IsTrue(sitcom.Episodes.Select(e => e.Season).All(s => s == 1)); // Check that sitcomManger didn't changed defualt season number
        }

        [TestMethod]
        public void AddNewSeasonToSitcom_SpecifySeason()
        {
            string name = "foo";
            string fileName = "fooFile";
            int seasonNumber = 5;
            Sitcom sitcom = new Sitcom() { Name = name };

            _EpisodeParserMock.Setup(e => e.Parse(It.Is<string>(s => s == fileName))).Returns(_Episodes);
            _SitcomRepository.Setup(r => r.Find(It.IsAny<PredicateType>())).Returns(new List<Sitcom>() { sitcom });
            _SitcomManager.Add(name, seasonNumber, fileName);

            _SitcomRepository.Verify(r => r.Add(It.Is<Sitcom>(s => s.Name == name)), Times.Never);
            Assert.IsTrue(sitcom.Episodes.Select(e => e.Season).All(s => s == seasonNumber));
        }
    }
}
