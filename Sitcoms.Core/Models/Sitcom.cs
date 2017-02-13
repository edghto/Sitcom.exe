using System.Collections.Generic;

namespace Sitcoms.Core.Models
{
    public class Sitcom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }

        public Sitcom()
        {
            Episodes = new List<Episode>();
        }
    }
}
