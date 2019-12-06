using System.Linq.Expressions;
using System.Reflection;
using ConsoleApp1.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace ConsoleApp1.Functions
{
    public class TranslateImpl : IMethodCallTranslator
    {

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

        public Expression Translate(MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method == _encryptMethod)
            {
                var password = methodCallExpression.Arguments[1];
                var value = methodCallExpression.Arguments[2];
                return new EncryptExpression(password, value);
            }
            if (methodCallExpression.Method == _decryptMethod)
            {
                var password = methodCallExpression.Arguments[1];
                var value = methodCallExpression.Arguments[2];
                return new DecryptExpression(password, value);
            }

            if (methodCallExpression.Method == _decryptByKeyMethod)
            {
                var value = methodCallExpression.Arguments[1];
                return new DecryptByKeyExpression(value);
            }

            return null;
        }
    }
}

