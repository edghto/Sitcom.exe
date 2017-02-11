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
        public void Add(string name, int season, string sourceFile)
        {
            throw new NotImplementedException();
        }

        public ICollection<Sitcom> List()
        {
            throw new NotImplementedException();
        }

        public void Report(params ReportRequest[] requests)
        {
            throw new NotImplementedException();
        }

        public void SetLast(string name, int season, int last)
        {
            throw new NotImplementedException();
        }
    }
}
