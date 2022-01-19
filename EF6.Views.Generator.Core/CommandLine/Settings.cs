namespace EF6.Views.Generator.Core.CommandLine
{
    public class Settings
    {
        public string DllPath { get; }

        public string ContextName { get; }

        public string OutputPath { get; }

        public Settings(string dllPath, string contextName, string outputPath)
        {
            DllPath = dllPath;
            ContextName = contextName;
            OutputPath = outputPath;
        }
    }
}
