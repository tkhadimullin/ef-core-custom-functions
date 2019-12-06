using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1.EFExtensions
{
    public class SqlServerDbContextOptionsExtension : IDbContextOptionsExtension
    {
        public string LogFragment => "'EncrypionSupport'=true";

        public bool ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<ICompositeMethodCallTranslator, SqlServerMethodCallTranslatorPlugin>();
            
            return false;
        }

        public long GetServiceProviderHashCode()
        {
            return 0;
        }

        public void Validate(IDbContextOptions options)
        {
        }
    }
}
