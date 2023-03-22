using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Infrastructure.Interfaces.Base
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        void RejectChanges();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext GetDbContext();
    }
}
