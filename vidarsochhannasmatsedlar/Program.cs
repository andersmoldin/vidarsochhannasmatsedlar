using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ServiceStack;
using Skolmaten;

namespace vidarsochhannasmatsedlar
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (Province province in API.GetProvinces())
            {
                Console.WriteLine($"{province.Id}: {province.Name}");
            }

            Console.WriteLine();

            foreach (District district in API.GetDistrict(5758661280399360.ToString()))
            {
                Console.WriteLine($"{district.Id}: {district.Name}");
            }

            Console.WriteLine();

            foreach (School school in API.GetSchool(189001.ToString()))
            {
                Console.WriteLine($"{school.Id}: {school.Name}");
            }

            Console.WriteLine();

            foreach (Week week in API.GetMenu(5605611798528000.ToString()))
            {
                foreach (var day in week.Days)
                {
                    foreach (var item in day.Items)
                    {
                        Console.WriteLine(item);
                    }
                }
            }

            var hej = $"https://skolmaten.se/api/3/schools/?district=189001"
                .GetJsonFromUrl(webReq =>
                {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Environment.GetEnvironmentVariable("token");
                });
            //Console.WriteLine($"Token: {Environment.GetEnvironmentVariable("token")}, versionsID: {Environment.GetEnvironmentVariable("versionsID")}");
            
            //var hej = $"https://skolmaten.se/api/3/schools/?district=189001"
            //    .GetJsonFromUrl(webReq =>
            //    {
            //        webReq.Headers["User-Agent"] = "request";
            //        webReq.Headers["Client"] = Environment.GetEnvironmentVariable("token");
            //    });

            //Menu menu = $"https://skolmaten.se/api/3/menu/?school=5605611798528000"
                //.GetJsonFromUrl(webReq => {
                //    webReq.Headers["User-Agent"] = "request";
                //    webReq.Headers["Client"] = Environment.GetEnvironmentVariable("token");
                //})
                //.FromJson<Menu>();

            Console.WriteLine($"Veckans mat för {menu.School.Name + Environment.NewLine}");

            foreach (var week in menu.Weeks)
            {
                foreach (var day in week.Days)
                {
                    var date = DateTimeOffset.FromUnixTimeSeconds(day.Date).DateTime.ToString(@"dddd dd\/MM", CultureInfo.CreateSpecificCulture("sv-se"));

                    Console.WriteLine($"  {char.ToUpper(date[0]) + date.Substring(1)}");

                    foreach (var item in day.Items)
                    {
                        Console.WriteLine($"- {item}");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
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
}
