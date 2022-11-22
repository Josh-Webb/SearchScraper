using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using SearchScraper.Models;

namespace SearchScraper.Controllers
{
    public class WebScraper : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string callurl(string url, string keywords)
        {
            //Main call.
            //Initialize the dictionary that will hold all urls and an array
            Dictionary<string, List<int>> urlDictionary = new Dictionary<string, List<int>>();
            urlDictionary = returnMatchCollectionDict(returnMatchCollection(returnWebResponseString(keywords)));


            if (urlDictionary.ContainsKey(url))
            {
                return $"{string.Join(", ", urlDictionary.GetValueOrDefault(url)): 0}";
            }
            else
            {
                return $"0";
            }
        }
        
        public string returnWebResponseString(string keywords)
        {
            string urlRequest = "https://www.google.com/search?num=100&q=" + keywords;
            WebRequest request = HttpWebRequest.Create(urlRequest);
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }

        public MatchCollection returnMatchCollection(string responseAsText)
        {
            Regex r = new Regex("\\bBNeawe UPmit AP7Wnd( lRVwie\">|\">)([^\\s<]+)");
            return r.Matches(responseAsText);
        }

        public Dictionary<string, List<int>> returnMatchCollectionDict(MatchCollection mc){
            Dictionary<string, List<int>> urlDictionary = new Dictionary<string, List<int>>();
            int matchCount = 0;
            foreach (Match m in mc)
            {
                matchCount++;
                if (urlDictionary.ContainsKey(m.Groups[2].Value))
                {
                    urlDictionary[m.Groups[2].Value].Add(matchCount);
                }
                else
                {
                    urlDictionary.Add(m.Groups[2].Value, new List<int> { matchCount });
                }
            }
            return urlDictionary;
        }

    }
}
