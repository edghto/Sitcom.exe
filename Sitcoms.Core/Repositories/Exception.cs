using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitcoms.Core.Repositories
{
    public class SitcomNotFoundException
        : System.Exception
    {
        public int MyProperty { get; set; }

        public SitcomNotFoundException()
            : base("Sitcom not found")
        {}

        public SitcomNotFoundException(string message)
            : base(message)
        { }

        public SitcomNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class SeasonNotFoundException
        : System.Exception
    {
        public int MyProperty { get; set; }

        public SeasonNotFoundException()
            : base("Season not found")
        { }

        public SeasonNotFoundException(string message)
            : base(message)
        { }

        public SeasonNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class EpisodeNotFoundException
        : System.Exception
    {
        public int MyProperty { get; set; }

        public EpisodeNotFoundException()
            : base("Episode not found")
        { }

        public EpisodeNotFoundException(string message)
            : base(message)
        { }

        public EpisodeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
