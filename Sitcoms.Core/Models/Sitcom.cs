using System.Collections.Generic;

namespace Sitcoms.Core.Models
{
    public class Sitcom
    {
        public string Name { get; set; }
        public ICollection<Season> Seasons { get; set; }
    }
}
