using System;
using System.Collections;

namespace vidarsochhannasmatsedlar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Token: {Environment.GetEnvironmentVariable("token")}, versionsID: {Environment.GetEnvironmentVariable("versionsID")}");
        }
    }
}
