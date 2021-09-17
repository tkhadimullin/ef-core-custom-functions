using System;
using System.Linq;
using ConsoleApp1.Repos;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerFactory myLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
            var repo = new Repo(new MyDbContext(myLoggerFactory));
            repo.SymmetricKeyName = "TestKeyWithPassword";
            repo.SymmetricKeyPassword = "TestPassword!@#"; // Newer version of SQL Server would refuse to take weak passwords
            var model = repo.GetAllById(1);
            Console.WriteLine();
            Console.WriteLine(model.First().Decrypted);
            Console.WriteLine(model.First().Decrypted2);
            Console.ReadKey();
        }
    }
}
