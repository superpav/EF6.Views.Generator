using System;
using System.Collections.Generic;

namespace EF6.Views.Generator.Core.CommandLine
{
    public class CommandLineParser
    {
        private readonly IReadOnlyList<string> _parameterNames;

        public CommandLineParser()
        {
            _parameterNames = new[]
            {
                "dllPath",
                "contextName",
                "outputPath"
            };
        }

        public string Validate(string[] arguments)
        {
            var missingParameters = new List<string>();

            var argumentsSet = new HashSet<string>(arguments, StringComparer.InvariantCultureIgnoreCase);

            foreach (var parameterName in _parameterNames)
            {
                if (!argumentsSet.Contains(parameterName))
                    missingParameters.Add(parameterName);
            }

            if (missingParameters.Count != 0)
                return $"Required parameters have not been specified: {string.Join(", ", missingParameters)}";

            if (arguments.Length < _parameterNames.Count * 2)
                return $"Some parameters do not have a value!";

            return string.Empty;
        }

        public Settings Parse(string[] arguments)
        {
            var i = 0;

            var parameters = new string[_parameterNames.Count];

            while (i < arguments.Length)
            {
                var argument = arguments[i];

                if (argument.Equals(_parameterNames[0], StringComparison.InvariantCultureIgnoreCase))
                {
                    parameters[0] = arguments[++i];
                }
                else if (argument.Equals(_parameterNames[1], StringComparison.InvariantCultureIgnoreCase))
                {
                    parameters[1] = arguments[++i];
                }
                else if (argument.Equals(_parameterNames[2], StringComparison.InvariantCultureIgnoreCase))
                {
                    parameters[2] = arguments[++i];
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Argument '{argument}' is not supported");
                }

                i++;
            }

            return new Settings(
                parameters[0],
                parameters[1],
                parameters[2]);
        }
    }
}
