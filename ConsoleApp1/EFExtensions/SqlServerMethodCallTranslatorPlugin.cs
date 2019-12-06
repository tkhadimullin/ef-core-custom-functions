using System.Collections.Generic;
using ConsoleApp1.Functions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;

namespace ConsoleApp1.EFExtensions
{

    public class SqlServerMethodCallTranslatorPlugin : SqlServerCompositeMethodCallTranslator
    {
        public SqlServerMethodCallTranslatorPlugin(
            RelationalCompositeMethodCallTranslatorDependencies dependencies)
            : base(dependencies)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddTranslators(new List<IMethodCallTranslator>
            {
                new TranslateImpl()
            });
        }
    }
}
