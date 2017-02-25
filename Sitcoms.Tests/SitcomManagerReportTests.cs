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
    public class SitcomManagerReportTests
    {
        private SitcomsManager _SitcomManager;
        private Mock<ISitcomRepository> _SitcomRepository;
        private Mock<IEpisodeRepository> _EpisodeRepository;
        private Mock<IUnitOfWork> _UnitOfWork;

        [TestInitialize()]
        public void Initialize()
        {
            _SitcomRepository = new Mock<ISitcomRepository>();
            _EpisodeRepository = new Mock<IEpisodeRepository>();
            _UnitOfWork = new Mock<IUnitOfWork>();
            _SitcomManager = new SitcomsManager(_UnitOfWork.Object);

            _UnitOfWork.Setup(s => s.SitcomRepository).Returns(_SitcomRepository.Object);
            _UnitOfWork.Setup(s => s.EpisodeRepository).Returns(_EpisodeRepository.Object);
        }

        [TestMethod]
        public void GetAll()
        {
            var sitcoms = new List<Sitcom>() {
                new Sitcom { Name = "foo" },
                new Sitcom { Name = "boo" },
                new Sitcom { Name = "bar" },
            };
            _SitcomRepository.Setup(s => s.GetAll()).Returns(sitcoms);

            List<ReportRequest> requests = new List<ReportRequest>();
            _EpisodeRepository
                .Setup(e => e.GetEpisodesByRequest(It.IsAny<ReportRequest[]>()))
                .Callback((ReportRequest[] r) => requests.AddRange(r));

            _SitcomManager.Report();
            _SitcomRepository.Verify(s => s.GetAll());
            _EpisodeRepository.Verify(e => e.GetEpisodesByRequest(
                It.Is<ReportRequest[]>(r => r.Count() == 3)));

            var expected = new HashSet<string>(sitcoms.Select(s => s.Name));
            var actual = new HashSet<string>(requests.Select(r => r.Name));
            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var name in actual)
            {
                Assert.IsTrue(expected.Contains(name));
            }
        }


        [TestMethod]
        public void GetSpecific()
        {
            _SitcomManager.Report(new ReportRequest() { Name = "foo", Season = 1 });
            _EpisodeRepository.Verify(e => e.GetEpisodesByRequest(
                It.Is<ReportRequest[]>(r => r.Count() == 1 && r[0].Name == "foo" && r[0].Season == 1)));
        }
    }
}
