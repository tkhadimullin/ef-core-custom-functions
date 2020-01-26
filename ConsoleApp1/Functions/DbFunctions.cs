using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ConsoleApp1.Functions
{
    public class TranslateImpl : IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _expressionFactory;

        private static readonly MethodInfo _encryptMethod
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.Encrypt),
                new[] { typeof(DbFunctions), typeof(string), typeof(string) });
        private static readonly MethodInfo _decryptMethod
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.Decrypt),
                new[] { typeof(DbFunctions), typeof(string), typeof(byte[]) });

        private static readonly MethodInfo _decryptByKeyMethod
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.DecryptByKey),
                new[] { typeof(DbFunctions), typeof(byte[]) });

        public TranslateImpl(ISqlExpressionFactory expressionFactory)
        {
            _expressionFactory = expressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            var args = new List<SqlExpression> { arguments[1], arguments[2] }; // cut the first parameter from extension function
            if (method == _encryptMethod)
            {
                return _expressionFactory.Function(instance, "EncryptByPassPhrase", args, typeof(byte[]));
            }
            if (method == _decryptMethod)
            {
                return _expressionFactory.Function(instance, "DecryptByPassPhrase", args, typeof(byte[]));
            }

            if (method == _decryptByKeyMethod)
            {
                return _expressionFactory.Function(instance, "DecryptByKey", args, typeof(byte[]));
            }

            return null;
        }
    }
}

