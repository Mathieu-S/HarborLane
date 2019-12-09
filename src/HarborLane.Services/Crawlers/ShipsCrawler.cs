using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarborLane.Domain.Crawlers;
using HarborLane.Domain.Models;
using HtmlAgilityPack;

namespace HarborLane.Services.Crawlers
{
    public class ShipsCrawler : IShipsCrawler
    {
        private const string UrlShipList = "https://azurlane.koumakan.jp/List_of_Ships";
        
        public async Task<IEnumerable<Ship>> GetAllShipsAsync()
        {
            var web = new HtmlWeb();
            HtmlDocument htmlDoc;

            try
            {
                htmlDoc = await web.LoadFromWebAsync(UrlShipList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var standardShipList = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div/table[1]")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            var researchShipList = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div/table[2]")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            var collabShipList = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div/table[3]")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            var retrofittedShipList = htmlDoc.DocumentNode.SelectSingleNode("//*[@id='mw-content-text']/div/table[4]")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            var mergedShipList = standardShipList
                .Concat(researchShipList)
                .Concat(collabShipList)
                .Concat(retrofittedShipList)
                .ToList();

            return mergedShipList.Select(shipInfo => new Ship()
                {
                    Id = shipInfo[0],
                    Name = shipInfo[1],
                    Rarity = shipInfo[2],
                    Type = shipInfo[3],
                    Affiliation = shipInfo[4]
                })
                .ToList();
        }
    }
}