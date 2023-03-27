using Backend.TechChallenge.Infrastructure.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace Backend.TechChallenge.Infrastructure.Base
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext dbContext;

        public UnitOfWork(TContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TContext GetDbContext()
        {
            return dbContext;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void RejectChanges()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
