namespace EF6.Views.Generator.Core.CommandLine
{
    public class Settings
    {
        public string DllFile { get; }

        public string ContextName { get; }

        public string OutputPath { get; }

        public string ConfigFile { get; set; }

        public string ConnectionStringName { get; set; }

        public Settings(string dllPath, string contextName, string outputPath, string configPath, string connectionStringName)
        {
            DllFile = dllPath;
            ContextName = contextName;
            OutputPath = outputPath;
            ConfigFile = configPath;
            ConnectionStringName = connectionStringName;
        }
    }
}
