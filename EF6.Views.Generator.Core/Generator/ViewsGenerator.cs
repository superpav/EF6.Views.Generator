using Microsoft.DbContextPackage.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EF6.Views.Generator.Core.Generator
{
    public class ViewsGenerator
    {
        public string Generate(dynamic dbContext, string outputPath)
        {
            Type dbContextType = dbContext.GetType();

            var objectContext = GetObjectContext(dbContext);

            var metadataWorkspace = objectContext.MetadataWorkspace;
            var getItemCollection = ((Type)metadataWorkspace.GetType()).GetMethod("GetItemCollection");
            var dataSpace = getItemCollection.GetParameters().First().ParameterType;

            var mappingCollection = getItemCollection.Invoke(
                metadataWorkspace,
                new[] { Enum.Parse(dataSpace, "CSSpace") });

            var mappingCollectionType = (Type)mappingCollection.GetType();

            var edmSchemaError = mappingCollectionType.Assembly
                        .GetType("System.Data.Entity.Core.Metadata.Edm.EdmSchemaError", true);

            var listOfEdmSchemaError = typeof(List<>).MakeGenericType(edmSchemaError);

            var errors = Activator.CreateInstance(listOfEdmSchemaError);

            var views = mappingCollectionType
                .GetMethod("GenerateViews", new[] { listOfEdmSchemaError })
                .Invoke(mappingCollection, new[] { errors });

            foreach (var error in (IEnumerable<dynamic>)errors)
            {
                if ((int)error.Severity == 1)
                {
                    throw new Exception(error.ToString());
                }
            }

            var viewGenerator = new CSharpViewGenerator
            {
                ContextTypeName = dbContextType.FullName,
                MappingHashValue = mappingCollection.ComputeMappingHashValue(),
                Views = views
            };

            var viewsFileName = $"{dbContextType.Name}.Views.cs";

            var viewsPath = Path.Combine(outputPath, viewsFileName);

            File.WriteAllText(viewsPath, viewGenerator.TransformText());

            return viewsPath;
        }

        private static dynamic GetObjectContext(dynamic context)
        {
            var objectContextAdapterType = context.GetType()
                .GetInterface("System.Data.Entity.Infrastructure.IObjectContextAdapter");

            return objectContextAdapterType.InvokeMember("ObjectContext", BindingFlags.GetProperty, null, context, null);
        }
    }
}
