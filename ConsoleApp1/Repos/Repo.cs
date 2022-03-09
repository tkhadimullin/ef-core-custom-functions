using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Functions;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repos
{
    public class Repo : BaseRepo<Model>
    {
        public IEnumerable<Model> GetAllById(int id)
        {
            // Transaction to keep together the two ExecuteSqlRaw with the query
            DbContext.Database.BeginTransaction();

            // you will need to uncomment the following line to work with your key
            DbContext.Database.ExecuteSqlRaw($"DECLARE @Open NVARCHAR(MAX) = N'OPEN SYMMETRIC KEY ' + QUOTENAME(@p0, '[') + ' DECRYPTION BY PASSWORD = ' + QUOTENAME(@p1, '''') + N';'; EXEC sp_executesql @Open", SymmetricKeyName, SymmetricKeyPassword);

            var filteredSet = Set.Include(x => x.Table2)
                .Where(x => x.Id == id)
                .Where(x => x.Table2.IsSomething)
                .Select(m => new Model
                {
                    Id = m.Id,
                    Decrypted = MyDbContext.DecryptByPassphrase("TestPassword", m.Encrypted).ToString(),
                    Decrypted2 = MyDbContext.DecryptByKey(m.Encrypted2).ToString(), // since the key's opened for session scope - just relying on it should do the trick
                    Table2 = m.Table2,
                    Encrypted = m.Encrypted,
                    Encrypted2 = m.Encrypted2
                }).ToList();

            // you will need to uncomment the following line to work with your key
            DbContext.Database.ExecuteSqlRaw($"DECLARE @Close NVARCHAR(MAX) = N'CLOSE SYMMETRIC KEY ' + QUOTENAME(@p0, '[') + ';'; EXEC sp_executesql @Close", SymmetricKeyName);

            DbContext.Database.CommitTransaction();

            return filteredSet;
        }

        public Repo(MyDbContext context) : base(context)
        {
        }
    }
}
