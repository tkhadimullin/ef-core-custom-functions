using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ConsoleApp1.EFExtensions
{
    public class SqlServerDbContextOptionsExtension : IDbContextOptionsExtension
    {
        private DbContextOptionsExtensionInfo _info;

        public void Validate(IDbContextOptions options)
        {
        }

        public DbContextOptionsExtensionInfo Info
        {
            get
            {
                return this._info ??= new MyDbContextOptionsExtensionInfo(this);
            }
        }

        void IDbContextOptionsExtension.ApplyServices(IServiceCollection services)
        {
            // this does not work and I don't know why
            //services.TryAddSingleton<IMethodCallTranslatorProvider, CustomSqlServerMethodCallTranslatorPlugin>(); 
            services.AddScoped<IMethodCallTranslatorProvider, CustomSqlServerMethodCallTranslatorPlugin>();
        }

        private sealed class MyDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
        {
            public MyDbContextOptionsExtensionInfo(IDbContextOptionsExtension instance) : base(instance) { }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => "";

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
            }

            public override int GetServiceProviderHashCode()
            {
                return 0;
            }

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => false;
        }
    }
}
