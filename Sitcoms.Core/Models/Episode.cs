using System;

namespace Sitcoms.Core.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime PremiereDate { get; set; }
        public int Season { get; set; }
        public bool Watched { get; set; }

        public virtual Sitcom Sitcom { get; set; }
    }
}
