using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Repositories.ISitcomRepository SitcomRepository { get; }
        Repositories.IEpisodeRepository EpisodeRepository { get; }

        int Complete();
    }
}
