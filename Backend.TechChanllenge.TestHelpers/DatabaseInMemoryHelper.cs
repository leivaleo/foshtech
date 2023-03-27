using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Backend.TechChallenge.TestHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.TechChanllenge.TestHelpers
{
    public static class DatabaseInMemoryHelper
    {
        public static TechCallengeDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TechCallengeDbContext>()
                .UseInMemoryDatabase("TechChallengeTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new TechCallengeDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            PopulateDb.UserTable(context);
            
            context.SaveChanges();
            return context;
        }
    }
}
