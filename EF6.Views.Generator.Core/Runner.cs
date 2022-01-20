using EF6.Views.Generator.Core.CommandLine;
using EF6.Views.Generator.Core.DbContext;
using EF6.Views.Generator.Core.Generator;
using System;
using System.Reflection;

namespace EF6.Views.Generator.Core
{
    public static class Runner
    {
        public static void Run(string[] args, Func<Settings, string> getConnectionString)
        {
            var parser = new CommandLineParser();
            var dbContextProvider = new DbContextProvider();
            var generator = new ViewsGenerator();

            var validationResult = parser.Validate(args);

            if (validationResult != string.Empty)
            {
                Console.WriteLine(validationResult);
                return;
            }

            var settings = parser.Parse(args);

            var connectionString = settings.ConfigFile != null
                ? getConnectionString(settings)
                : null;

            var assembly = Assembly.LoadFrom(settings.DllFile);

            var dbContext = dbContextProvider.GetDbContextInstance(assembly, settings.ContextName, connectionString);

            var viewsPath = generator.Generate(dbContext, settings.OutputPath);

            Console.WriteLine("Success!");
            Console.WriteLine(viewsPath);
        }
    }
}
