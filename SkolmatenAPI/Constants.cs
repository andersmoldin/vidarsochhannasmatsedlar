using System;
namespace Skolmaten
{
    public static class Constants
    {
        public static string Client { get { return Environment.GetEnvironmentVariable("token"); } }

        public static string Address { get; } = "https://skolmaten.se/api/3/";
    }
}
