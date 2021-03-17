using System;
using Microsoft.EntityFrameworkCore;
using Viseo.Application.Common.Interfaces;
using Viseo.Persistence;

namespace Viseo.Application.UnitTests.Common
{
    public class DbContextFactory
    {
        public static ApplicationDbContext Create(ICurrentUserService currentUser)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options, currentUser);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}