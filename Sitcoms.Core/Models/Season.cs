using System.Collections.Generic;

namespace Sitcoms.Core.Models
{
    public class Season
    {
        public int Number { get; set; }
        public ICollection<Episode> Episodes { get; set; }
        public int? Last { get; set; }

        public Season()
        {
            Episodes = new List<Episode>();
        }
    }
}
