using CommandLine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sitcoms
{
    [Verb("list", HelpText= "List stored sitcoms with all seasons")]
    class ListOptions
    {
    }

    [Verb("report", HelpText = "Report requested sitcoms")]
    class ReportOptions
    {

        [Value(0, HelpText = "Names of requested sitcoms, accepted formats name[:season]")]
        public IEnumerable<string> RawRequests
        {
            get
            {
                if (_RawRequests == null)
                    _RawRequests = new List<string>();
                return _RawRequests;
            }
            set
            {
                _RawRequests = value;
            }
        }
        private IEnumerable<string> _RawRequests;

        public ICollection<Core.ReportRequest> Requests
        {
            get
            {
                var requests = new List<Core.ReportRequest>();
                foreach (var r in RawRequests)
                {
                    var regex = new Regex(@"^\s*([\w-\s]+)(:(\d+))?\s*$");
                    var match = regex.Match(r);
                    if (!match.Success) throw new Exception("Invalid request format " + r);

                    var request = new Core.ReportRequest();
                    request.Name = match.Groups[1].Value;
                    if (match.Groups[3].Value.Length != 0)
                        request.Season = int.Parse(match.Groups[3].Value);

                    requests.Add(request);
                }
                return requests;
            }
        }
    }

    [Verb("last", HelpText = "Set last watched episode of sitcom")]
    class LastOptions
    {
        [Option('s', "season", Required = true,
            HelpText = "Season number")]
        public int Season { get; set; }

        [Value(0, Required = true,
            HelpText = "Sitcom name")]
        public string Name { get; set; }

        [Value(1, Required = true,
            HelpText = "Episode number")]
        public int Last { get; set; }
    }

    
    [Verb("add", HelpText = "Add new season to sitcom")]
    class AddOptions
    {
        [Option('f', "file", Required = true,
            HelpText = "Source file name")]
        public string SourceFile { get; set; }

        [Option('s', "season", Required = false,
            HelpText = "Season number (override season extracted from file)")]
        public int? Season { get; set; }

        [Value(0, Required = true,
            HelpText = "Sitcom name")]
        public string Name { get; set; }
    }


    [Verb("delete", HelpText = "Delete sitcom or season of sitcom")]
    class DeleteOptions
    {
        [Option('s', "season", Required = false,
            HelpText = "Season number")]
        public int? Season { get; set; }

        [Value(0, Required = true,
            HelpText = "Sitcom name")]
        public string Name { get; set; }
    }
}
