using System;
using System.Collections.Generic;
using ServiceStack;

namespace Skolmaten
{
    public static class API
    {
        public static List<Province> GetProvinces(){
            var response = (Constants.Address + $"provinces")
                .GetJsonFromUrl(webReq =>
                {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Constants.Client;
                }).FromJson<Response>();

            return response.Provinces;
        }

        public static List<District> GetDistrict(string province)
        {
            var response = (Constants.Address + $"districts/?province={province}")
                .GetJsonFromUrl(webReq =>
                {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Constants.Client;
                }).FromJson<Response>();

            return response.Districts;
        }

        public static List<School> GetSchool(string district)
        {
            var response = (Constants.Address + $"schools/?district={district}")
                .GetJsonFromUrl(webReq =>
                {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Constants.Client;
                }).FromJson<Response>();

            return response.Schools;
        }

        public static List<Week> GetMenu(string school, int? offset = null, int? limit = null, int? year = null, int? week = null)
        {
            //string hej = Constants.Address + $"menu/?school={school}{((offset != null) ? "&offset=" + offset : null)}{((limit != null) ? "&limit=" + limit : null)}";

            var response = (Constants.Address + $"menu/?school={school}{((offset != null) ? "&offset=" + offset : null)}{((limit != null) ? "&limit=" + limit : null)}")
                .GetJsonFromUrl(webReq =>
                {
                    webReq.Headers["User-Agent"] = "request";
                    webReq.Headers["Client"] = Constants.Client;
                }).FromJson<Response>();

            return response.Weeks;
        }
    }

    public class Response
    {
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
        public List<School> Schools { get; set; }
        public List<Week> Weeks { get; set; }
    }

    public class Menu
    {
        public List<Week> Weeks { get; set; }
        public School School { get; set; }
        public List<string> Bulletins { get; set; }
    }

    public class School
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string URLName { get; set; }
        public string ImageURL { get; set; }
        public District District { get; set; }
    }

    public class District
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string URLName { get; set; }
        public Province Province { get; set; }
    }

    public class Province
    {
        public string Id { get; set; }
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
