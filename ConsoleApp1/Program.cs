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
            var repo = new Repo(new MyDbContext(new LoggerFactory().AddConsole()));
            var model = repo.GetAllById(1);
            Console.WriteLine(model.First().Decrypted);
            Console.ReadKey();
        }
    }
}
