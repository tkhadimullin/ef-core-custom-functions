using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;

namespace ConsoleApp1.Expressions
{
    public class DecryptExpression : EncryptExpression
    {
        public override Type Type => typeof(string);
        protected override Expression Accept(ExpressionVisitor visitor)
        {
            if (!(visitor is IQuerySqlGenerator))
            {
                return base.Accept(visitor);
            }

            visitor.Visit(new SqlFragmentExpression("CAST(DECRYPTBYPASSPHRASE("));

            visitor.Visit(Password);
            visitor.Visit(new SqlFragmentExpression(", "));
            visitor.Visit(Value);

            visitor.Visit(new SqlFragmentExpression(") AS VARCHAR(MAX))"));

            return this;
        }

        public DecryptExpression(Expression password, Expression value) : base(password, value)
        {
        }
    }
}