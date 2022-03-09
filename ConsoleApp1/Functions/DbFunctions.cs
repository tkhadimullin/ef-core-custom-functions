using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ConsoleApp1.Functions
{
    public class TranslateImpl : IMethodCallTranslator
    {
        private readonly ISqlExpressionFactory _expressionFactory;

        private static readonly MethodInfo _encryptByPassphraseMethod
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.EncryptByPassphrase),
                new[] { typeof(DbFunctions), typeof(string), typeof(string) });

        private static readonly MethodInfo _decryptByPassphraseMethod
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.DecryptByPassphrase),
                new[] { typeof(DbFunctions), typeof(string), typeof(byte[]) });

        private static readonly MethodInfo _decryptByKeyMethod
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.DecryptByKey),
                new[] { typeof(DbFunctions), typeof(byte[]) });

        private static readonly MethodInfo _convertToVarChar1
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.ConvertToVarChar),
                new[] { typeof(DbFunctions), typeof(byte[]) });

        private static readonly MethodInfo _convertToVarChar2
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.ConvertToVarChar),
                new[] { typeof(DbFunctions), typeof(byte[]), typeof(int) });

        private static readonly MethodInfo _convertToNVarChar1
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.ConvertToNVarChar),
                new[] { typeof(DbFunctions), typeof(byte[]) });

        private static readonly MethodInfo _convertToNVarChar2
            = typeof(DbFunctionsExtensions).GetMethod(
                nameof(DbFunctionsExtensions.ConvertToNVarChar),
                new[] { typeof(DbFunctions), typeof(byte[]), typeof(int) });

        public TranslateImpl(ISqlExpressionFactory expressionFactory)
        {
            _expressionFactory = expressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments, IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (method == _encryptByPassphraseMethod)
            {
                var args = new[] { arguments[1], arguments[2] }; // cut the first parameter from extension function
                return _expressionFactory.Function("ENCRYPTBYPASSPHRASE", args, true, new[] { true, true }, typeof(byte[]));
            }

            if (method == _decryptByPassphraseMethod)
            {
                var args = new[] { arguments[1], arguments[2] }; // cut the first parameter from extension function
                return _expressionFactory.Function("DECRYPTBYPASSPHRASE", args, true, new[] { true, true }, typeof(byte[]));
            }

            if (method == _decryptByKeyMethod)
            {
                var args = new[] { arguments[1], }; // cut the first parameter from extension function
                return _expressionFactory.Function("DECRYPTBYKEY", args, true, new[] { true, }, typeof(byte[]));
            }

            if (method == _convertToVarChar1 || method == _convertToVarChar2)
            {
                var len = arguments.Count == 3 ? ((SqlConstantExpression)arguments[2]).Value.ToString() : "MAX";
                var args = new[] { _expressionFactory.Fragment($"VARCHAR({len})"), arguments[1], };
                return _expressionFactory.Function("CONVERT", args, true, new[] { false, true }, typeof(string));
            }

            if (method == _convertToNVarChar1 || method == _convertToNVarChar2)
            {
                var len = arguments.Count == 3 ? ((SqlConstantExpression)arguments[2]).Value.ToString() : "MAX";
                var args = new[] { _expressionFactory.Fragment($"NVARCHAR({len})"), arguments[1], };
                return _expressionFactory.Function("CONVERT", args, true, new[] { false, true }, typeof(string));
            }

            return null;
        }
    }
}

