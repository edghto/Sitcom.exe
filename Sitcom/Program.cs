using System;
using System.Linq;
using CommandLine;

namespace Sitcoms
{
    class Program
    {
        static Sitcoms.Core.SitcomsManager GetSitcomsManager()
        {
            return new Core.SitcomsManager(new Sitcoms.Persistence.UnitOfWork());
        }

        static int RunAdd(AddOptions opts)
        {
            var sitcoms = GetSitcomsManager();
            sitcoms.Add(name: opts.Name, season: opts.Season, sourceFile: opts.SourceFile);
            return 0;
        }

        static int RunList(ListOptions opts)
        {
            var sitcoms = GetSitcomsManager();
            var list = sitcoms.List();
            foreach (var sitcom in list)
            {
                var seasons = sitcom.Seasons.Select(s => s.Number).ToList();
                Console.WriteLine("{0} {1}", sitcom.Name, string.Join(",", seasons));
            }
            return 0;
        }

        static int RunLast(LastOptions opts)
        {
            var sitcoms = GetSitcomsManager();
            sitcoms.SetLast(name: opts.Name, season: opts.Season, last: opts.Last);
            return 0;
        }

        static int RunReport(ReportOptions opts)
        {
            var requests = opts.Requests.ToArray();
            var sitcoms = GetSitcomsManager();
            var report = sitcoms.Report(requests);
            foreach (var sitcom in report)
            {
                Console.WriteLine(sitcom);
            }
            return 0;
        }

        static int Main(string[] args)
        {
            return Parser.Default
                .ParseArguments<AddOptions,ListOptions,LastOptions,ReportOptions>(args)
                .MapResult(
                    (AddOptions opts) => RunAdd(opts),
                    (ListOptions opts) => RunList(opts),
                    (LastOptions opts) => RunLast(opts),
                    (ReportOptions opts) => RunReport(opts),
                    errs => 1
                );
        }
    }
}
