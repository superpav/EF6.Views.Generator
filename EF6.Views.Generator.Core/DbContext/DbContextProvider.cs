using EF6.Views.Generator.Core.Extensions;
using System;
using System.Reflection;

namespace EF6.Views.Generator.Core.DbContext
{
    public class DbContextProvider
    {
        public object GetDbContextInstance(Assembly assembly, string contextName)
        {
            var dbContextType = assembly.GetDbContextType(contextName);

            var constructor = dbContextType.GetConstructor(new Type[0]);

            return constructor.Invoke(new object[0]);
        }
    }
}
