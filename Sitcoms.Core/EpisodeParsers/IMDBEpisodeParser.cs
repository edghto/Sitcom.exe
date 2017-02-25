using HtmlAgilityPack;
using Sitcoms.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sitcoms.Core.EpisodeParsers
{
    public class IMDBEpisodeParser
        : IEpisodeParser
    {
        private HtmlDocument doc;
        private int season;
        
        public ICollection<Episode> Parse(string fileName)
        {
            doc = new HtmlDocument();
            doc.Load(fileName);

            ICollection<Episode> episodes = new List<Episode>();
            season = GetSeason();

            foreach (var node in doc.DocumentNode.SelectNodes(@"//div[@class]"))
            {
                if (node.Attributes["class"].Value.Contains("list_item"))
                {
                    try
                    {
                        var episode = ParseEpisode(node);
                        episode.Season = season;
                        episodes.Add(episode);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("[Debug] parsing episode failed: {0}", e.Message);
                    }
                }
            }

            return episodes;
        }
        
        private Episode ParseEpisode(HtmlNode listItem)
        {
            var dateNode = listItem.SelectNodes(listItem.XPath + @"//div[@class='airdate']").First();
            var episodeNode = listItem.SelectNodes(listItem.XPath + @"//meta[@itemprop='episodeNumber']").First();
            var nameNode = listItem.SelectNodes(listItem.XPath + @"//a[@itemprop='name']").First();

            return new Episode()
            {
                Name = nameNode.Attributes["title"].Value,
                Number = Int32.Parse(episodeNode.Attributes["content"].Value),
                PremiereDate = DateTime.Parse(dateNode.InnerHtml),
            };
        }

        private int GetSeason()
        {
            var node = doc.DocumentNode.SelectNodes(@"//h3[@itemprop='name']").Single();
            var regex = new Regex(@"^Season.+(\d+)$");
            var match = regex.Match(node.InnerHtml);
            if (match.Success)
            {
                return Int32.Parse(match.Groups[1].Value);
            }

            throw new Exception();
        }
    }
}
