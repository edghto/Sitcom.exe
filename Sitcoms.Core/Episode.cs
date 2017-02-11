using System;

namespace Sitcoms.Core
{
    public class Episode
    {
        public int Number { get; set; }
        public int Season { get; set; }
        public string Name { get; set; }
        public DateTime PremiereDate { get; set; }
    }
}
