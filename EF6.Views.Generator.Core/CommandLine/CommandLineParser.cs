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
                "dllFile",
                "contextName",
                "outputPath",
                "configFile",
                "connectionStringName"
            };
        }

        public string Validate(string[] arguments)
        {
            var missingParameters = new List<string>();

            var argumentsSet = new HashSet<string>(arguments, StringComparer.InvariantCultureIgnoreCase);

            var requiredParameters = new[]
            {
                _parameterNames[0],
                _parameterNames[1],
                _parameterNames[2]
            };

            foreach (var parameterName in requiredParameters)
            {
                if (!argumentsSet.Contains(parameterName))
                    missingParameters.Add(parameterName);
            }

            if (missingParameters.Count != 0)
                return $"Required parameters have not been specified: {string.Join(", ", missingParameters)}";

            if (arguments.Length < requiredParameters.Length * 2)
                return $"Some required parameters do not have a value!";

            return string.Empty;
        }

        public Settings Parse(string[] arguments)
        {
            var i = 0;

            var parameters = new string[_parameterNames.Count];

            while (i < arguments.Length)
            {
                if (TrySetParameter(parameters, 0, arguments, i)) i++;
                else if (TrySetParameter(parameters, 1, arguments, i)) i++;
                else if (TrySetParameter(parameters, 2, arguments, i)) i++;
                else if (TrySetParameter(parameters, 3, arguments, i)) i++;
                else if (TrySetParameter(parameters, 4, arguments, i)) i++;
                else throw new ArgumentOutOfRangeException($"Argument '{arguments[i]}' is not supported");

                i++;
            }

            return new Settings(
                parameters[0],
                parameters[1],
                parameters[2],
                parameters[3],
                parameters[4]);
        }

        private bool TrySetParameter(string[] parameters, int parameterIndex, string[] arguments, int argumentIndex)
        {
            if (!arguments[argumentIndex].Equals(_parameterNames[parameterIndex], StringComparison.InvariantCultureIgnoreCase))
                return false;

            parameters[parameterIndex] = arguments[++argumentIndex];
            return true;
        }
    }
}
