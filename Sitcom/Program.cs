using System;
using System.Linq;
using CommandLine;

namespace Sitcoms
{
    class Program
    {
        static Core.IUnitOfWork _UnitOfWork = new Persistence.UnitOfWork();

        static Sitcoms.Core.SitcomsManager GetSitcomsManager()
        {
            return new Core.SitcomsManager(_UnitOfWork);
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
                Console.WriteLine("{0} {1}", sitcom.Name, string.Join(",", sitcom.Seasons));
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
            foreach (var season in report)
            {
                Console.WriteLine(season.Sitcom.Name);
                foreach (var episode in season.Episodes)
                {
                    Console.WriteLine("{0} S{1:00}E{2:00} - {3} ({4})", 
                        episode.Watched?"*":" ", 
                        episode.Season, 
                        episode.Number,
                        episode.Name,
                        episode.PremiereDate.ToShortDateString());
                }
            }
            return 0;
        }

        static int Main(string[] args)
        {
            try
            {
                return Parser.Default
                    .ParseArguments<AddOptions, ListOptions, LastOptions, ReportOptions>(args)
                    .MapResult(
                        (AddOptions opts) => RunAdd(opts),
                        (ListOptions opts) => RunList(opts),
                        (LastOptions opts) => RunLast(opts),
                        (ReportOptions opts) => RunReport(opts),
                        errs => 1
                    );
            }
            catch (Core.Repositories.SitcomNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Core.Repositories.SeasonNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Core.Repositories.EpisodeNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                _UnitOfWork.Dispose();
            }

            return -1;
        }
    }
}
