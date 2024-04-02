using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PrCSharp_lab3
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            int choice = -1;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("1) WhoIs");
            Console.WriteLine("2) Local Time");
            Console.WriteLine("3) Scraper");
            Console.WriteLine("4) Quit");

            Console.Write("Select option: ");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Write("Enter an ipv4 address: ");
                    string ipv4 = Console.ReadLine();
                    await WhoIs(ipv4);
                    break;
                case 2:
                    await LocalTime();
                    break;
                case 3:
                    Console.Write("Enter a keyword to omit: ");
                    string keyword = Console.ReadLine();
                    await WebScraper(keyword);
                    break;
                case 4:
                    break;
                default:
                    throw new ArgumentException("Wrong option selected");
            }
        }

        static async Task WhoIs(string ip)
        {
            string SiteUrl = $"https://ipapi.co/{ip}/country/";

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, SiteUrl);
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
        static async Task LocalTime()
        {
            string SiteUrl = "https://www.timeanddate.com/worldclock/bulgaria/sofia";
            HtmlDocument htmlDoc = await LoadHtmlDocumentAsync(SiteUrl);

            List<string> targetIds = new List<string> { "ctdat", "ct" };
            List<HtmlNode> foundNodes = new List<HtmlNode>();

            foreach (var id in targetIds)
            {
                HtmlNode node = htmlDoc.DocumentNode.Descendants().Where
                (x => (x.Name == "span" && x.Attributes["id"] != null &&
                x.Attributes["id"].Value.Equals(id))).ToList().First();
                foundNodes.Add(node);
            }

            Console.WriteLine(foundNodes[0].InnerText + " " + foundNodes[1].InnerText);
        }

        static async Task WebScraper(string keywordToSkip)
        {
            string SiteUrl = "https://www.mediapool.bg/news";
            HtmlDocument htmlDoc = await LoadHtmlDocumentAsync(SiteUrl);

            List<HtmlNode> articles = htmlDoc.DocumentNode.Descendants().Where
                (x => (x.Name == "article" && x.Attributes["id"] != null)).ToList();

            foreach (var article in articles)
            {
                if (article.Descendants().Where(
                    x => (x.Name == "a" && x.Attributes["href"] != null &&
                    x.Attributes["href"].Value.Contains(keywordToSkip))).ToList().Count != 0)
                {
                    continue;
                }

                var title = article.Descendants().Where
                    (x => (x.Name == "h2" && x.Attributes["class"] != null &&
                    x.Attributes["class"].Value.Equals("c-article-item__title"))).ToList();

                var dateTime = article.Descendants().Where
                    (x => (x.Name == "time" && x.Attributes["class"] != null &&
                    x.Attributes["class"].Value.Equals("c-article-item__date"))).ToList();

                if (title.Count != 0 && dateTime.Count != 0)
                {
                    Console.WriteLine(title[0].InnerText + " - " + dateTime[0].InnerText);
                }
            }
        }

        static async Task<HtmlDocument> LoadHtmlDocumentAsync(string SiteUrl) 
        {
            var response = await client.GetByteArrayAsync(SiteUrl);
            String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(source);

            return htmlDoc;
        }
    }
}