using System.Collections.Generic;
using ConsoleApp1.Functions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ConsoleApp1.EFExtensions
{
#pragma warning disable EF1001
    public sealed class CustomSqlServerMethodCallTranslatorPlugin : SqlServerMethodCallTranslatorProvider
#pragma warning restore EF1001
    {
        public CustomSqlServerMethodCallTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies)
            : base(dependencies)
        {
            ISqlExpressionFactory expressionFactory = dependencies.SqlExpressionFactory;

            AddTranslators(new List<IMethodCallTranslator>
            {
                new TranslateImpl(expressionFactory)
            });
        }
    }
}
