using EF6.Views.Generator.Core;
using EF6.Views.Generator.Core.CommandLine;
using Microsoft.Extensions.Configuration;

namespace EF6.Views.Generator.NET6
{
    internal class Program
    {
        private static string GetConnectionString(Settings settings)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(settings.ConfigFile)
                .Build();

            return config.GetConnectionString(settings.ConnectionStringName);
        }

        static void Main(string[] args)
        {
            Runner.Run(args, GetConnectionString);
        }
    }
}