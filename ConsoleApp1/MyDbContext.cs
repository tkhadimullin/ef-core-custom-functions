using ConsoleApp1.EFExtensions;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    public class MyDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<Model> Models { get; set; }
        public DbSet<Table2> Table2 { get; set; }

        public MyDbContext(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=test;Integrated Security=True",
                providerOptions => providerOptions.CommandTimeout(60)); // pretty standard stuff

            optionsBuilder.UseEncryptionFunctions(); // this is where the magic happens. invoking this extension lets EF know we've got some extra functions we can handle
            optionsBuilder.UseLoggerFactory(_loggerFactory); // this is optional I guess
        }
    }
}
