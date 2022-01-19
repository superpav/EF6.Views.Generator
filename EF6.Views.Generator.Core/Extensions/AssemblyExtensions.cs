using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EF6.Views.Generator.Core.Extensions
{
    public static class AssemblyExtensions
    {
        public static Type GetDbContextType(this Assembly assembly, string contextName)
        {
            return assembly.GetExportedTypes()
                .Where(x => x.Name == contextName)
                .FirstOrDefault(x => IsContextType(x));
        }

        private static IEnumerable<Type> GetBaseTypes(Type type)
        {
            while (type != typeof(object))
            {
                yield return type.BaseType;

                type = type.BaseType;
            }
        }

        private static bool IsContextType(Type userContextType)
        {
            return GetBaseTypes(userContextType)
                .Where(x => x.Assembly.GetName().Name == "EntityFramework")
                .Any(t => t.FullName == "System.Data.Entity.DbContext");
        }
    }
}
