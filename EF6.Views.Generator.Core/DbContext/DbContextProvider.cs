using EF6.Views.Generator.Core.Extensions;
using System;
using System.Reflection;

namespace EF6.Views.Generator.Core.DbContext
{
    public class DbContextProvider
    {
        public object GetDbContextInstance(Assembly assembly, string contextName, string connectionString)
        {
            var dbContextType = assembly.GetDbContextType(contextName);

            return connectionString == null
                ? dbContextType.GetConstructor(new Type[0]).Invoke(new object[0])
                : dbContextType.GetConstructor(new[] { typeof(string) }).Invoke(new[] { connectionString });
        }
    }
}
