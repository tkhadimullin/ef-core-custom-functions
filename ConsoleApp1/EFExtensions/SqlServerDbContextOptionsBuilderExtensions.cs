using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ConsoleApp1.EFExtensions
{
    public static class SqlServerDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseEncryptionFunctions(
            this DbContextOptionsBuilder optionsBuilder)
        {
            var extension = GetOrCreateExtension(optionsBuilder);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            return optionsBuilder;
        }

        private static SqlServerDbContextOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.Options.FindExtension<SqlServerDbContextOptionsExtension>()
               ?? new SqlServerDbContextOptionsExtension();
    }
}
