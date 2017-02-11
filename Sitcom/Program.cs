using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;

namespace Sitcoms
{
    class Program
    {
        static void Main(string[] args)
        {
            var sitcoms = new Sitcoms.Core.SitcomsManager();

            //Add new season to sitcom (sitcom may not exist)
            sitcoms.Add(name: "Foo", season: 1, sourceFile: @"e:\Download\foo.htm");
            sitcoms.Add(name: "Boo", season: 6, sourceFile: @"e:\Download\boo.htm");

            //List stored sitcoms with all seasons
            var l = sitcoms.List();
            foreach (var s in l)
            {
                Console.WriteLine("{0} {1}", s.Name, s.Seasons.Join(","));
            }

            //Set last watched episode of sitcom
            sitcoms.Last(name: "Foo", season: 1, last: 5);

            //Report requested sitcoms 
            sitcoms.Report(new Sitcoms.Core.ReportRequest() { Name = "Foo" }, new Sitcoms.Core.ReportRequest() { Name = "Boo", Season = 4 });
        }
    }
}
