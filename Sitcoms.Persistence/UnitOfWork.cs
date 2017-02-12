using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitcoms.Core.Repositories;
using Sitcoms.Persistence.Repositories;
using System.Data.Entity;

namespace Sitcoms.Persistence
{
    public class UnitOfWork : Core.IUnitOfWork
    {
        private SitcomsContext _Context = new SitcomsContext();

        public IEpisodeRepository EpisodeRepository { get; private set; }

        public ISeasonRepository SeasonRepository { get; private set; }

        public ISitcomRepository SitcomRepository { get; private set; }

        public int Complete()
        {
            return _Context.SaveChanges();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

        public UnitOfWork()
        {
            EpisodeRepository = new EpisodeRepository(_Context);
            SeasonRepository = new SeasonRepository(_Context);
            SitcomRepository = new SitcomRepository(_Context);
        }
    }
}
