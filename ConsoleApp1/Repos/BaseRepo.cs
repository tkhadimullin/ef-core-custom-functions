using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Repos
{
    public class BaseRepo<TEntity> where TEntity : class
    {
        protected MyDbContext DbContext { get; set; }
        public BaseRepo(MyDbContext context)
        {
            DbContext = context;
        }

        protected DbSet<TEntity> Set => DbContext.Set<TEntity>();

        public string SymmetricKeyPassword { get; set; }

        public string SymmetricKeyName { get; set; }
    }
}
