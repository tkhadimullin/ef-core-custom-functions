using System.Collections.Generic;
using ConsoleApp1.Functions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ConsoleApp1.EFExtensions
{

    public sealed class CustomSqlServerMethodCallTranslatorPlugin : SqlServerMethodCallTranslatorProvider
    {
        public CustomSqlServerMethodCallTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies) 
            : base(dependencies)
        {
            ISqlExpressionFactory expressionFactory = dependencies.SqlExpressionFactory;
            this.AddTranslators(new List<IMethodCallTranslator>
            {
                new TranslateImpl(expressionFactory)
            });
        }
    }
}
