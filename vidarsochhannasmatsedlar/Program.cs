using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;

namespace vidarsochhannasmatsedlar
{
    class Program
    {
        static void Main(string[] args)
        {
            var hej = $"https://skolmaten.se/api/3/schools/?district=189001"
                .GetJsonFromUrl(webReq =>
                {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Environment.GetEnvironmentVariable("token");
                });

            Menu menu = $"https://skolmaten.se/api/3/menu/?school=5605611798528000"
                .GetJsonFromUrl(webReq => {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Environment.GetEnvironmentVariable("token");
                })
                .FromJson<Menu>();

            Console.WriteLine($"Token: {Environment.GetEnvironmentVariable("token")}, versionsID: {Environment.GetEnvironmentVariable("versionsID")}");
        }

        public string GetReleases(string url)
        {
            var response = url.GetJsonFromUrl(webReq =>
            {
                webReq.UserAgent = "";
            });

            return response;
        }

        //public async Task<List<Menu>> GetDeserializedReleases(string url)
        //{
        //    var result = await "https://skolmaten.se"
        //        .AppendPathSegments("api", "3", "provinces")
        //        .SetQueryParams(new { Client = Environment.GetEnvironmentVariable("token"), ClientVersion = Environment.GetEnvironmentVariable("versionsID") })
        //        //.WithOAuthBearerToken("my_oauth_token")
        //        .GetStringAsync(new { first_name = firstName, last_name = lastName })
        //        .ReceiveString();

        //    return result;
        //}
    }

    internal class Response
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Menu
    {
        public List<Week> Weeks { get; set; }
        public School School { get; set; }
        public List<string> Bulletins { get; set; }
    }

    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URLName { get; set; }
        public string ImageURL { get; set; }
        public District District { get; set; }
    }

    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URLName { get; set; }
        public Province Province { get; set; }
    }

    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URLName { get; set; }
    }

    public class Week
    {
        public int Year { get; set; }
        public int Number { get; set; }
        public List<Day> Days { get; set; }
    }

    public class Day
    {
        public int Date { get; set; }
        public List<Meal> Meals { get; set; }
        public List<string> Items { get; set; }
    }

    public class Meal
    {
        public string Value { get; set; }
        public List<int> Attributes { get; set; }
    }
}
