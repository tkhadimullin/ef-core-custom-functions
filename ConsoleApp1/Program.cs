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
            var model = repo.GetAllById(1);
            Console.WriteLine(model.First().Decrypted);
            Console.ReadKey();
        }
    }
}
