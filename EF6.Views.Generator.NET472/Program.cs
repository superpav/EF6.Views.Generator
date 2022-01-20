using EF6.Views.Generator.Core;
using EF6.Views.Generator.Core.CommandLine;
using System;
using System.Configuration;

namespace EF6.Views.Generator.NET472
{
    internal class Program
    {
        private static string GetConnectionString(Settings settings)
        {
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                new ExeConfigurationFileMap { ExeConfigFilename = settings.ConfigFile },
                ConfigurationUserLevel.None);

            var connectionStrings = configuration.ConnectionStrings.ConnectionStrings;

            string connectionString = null;

            foreach(ConnectionStringSettings c in connectionStrings)
            {
                if (!c.Name.Equals(settings.ConnectionStringName, StringComparison.InvariantCultureIgnoreCase))
                    continue;

                connectionString = c.ConnectionString;
            }

            return connectionString;
        }

        static void Main(string[] args)
        {
            Runner.Run(args, GetConnectionString);
        }
    }
}
