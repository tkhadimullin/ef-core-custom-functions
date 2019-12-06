using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;

namespace ConsoleApp1.Expressions
{
    public class EncryptExpression : Expression
    {
        protected readonly Expression Value;
        protected readonly Expression Password;

        public override ExpressionType NodeType => ExpressionType.Extension;
        public override Type Type => typeof(byte[]);
        public override bool CanReduce => false;

        public EncryptExpression(Expression password, Expression value)
        {
            Value = value;
            Password = password;
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            var visitedValue = visitor.Visit(Value);
            var visitedPassword = visitor.Visit(Password);

            if (ReferenceEquals(Value, visitedValue))
            {
                return this;
            }

            if (ReferenceEquals(Password, visitedPassword))
            {
                return this;
            }

            return new EncryptExpression(visitedPassword, visitedValue);
        }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            if (!(visitor is IQuerySqlGenerator))
            {
                return base.Accept(visitor);
            }

            visitor.Visit(new SqlFragmentExpression("EncryptByPassPhrase("));

            visitor.Visit(Password);
            visitor.Visit(new SqlFragmentExpression(", "));
            visitor.Visit(Value);

            visitor.Visit(new SqlFragmentExpression(")"));

            return this;
        }
    }
}