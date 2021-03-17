using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Viseo.Application.Common.Interfaces;
using Viseo.Domain.Entities;
using Viseo.Persistence;

namespace Viseo.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context; 
        //protected readonly Mock<ICurrentUserService> _mockCurrentUser;
        protected readonly Mock<ICurrentUserService> _mockCurrentUser;
        protected readonly Mock<UserManager<AppUser>> _mockUserManager;
         protected readonly Mock<IMapper> _mockMapper;

        public CommandTestBase()
        {
            _mockCurrentUser = new Mock<ICurrentUserService>();
            _mockMapper = new Mock<IMapper>();
            _context = DbContextFactory.Create(_mockCurrentUser.Object);

            _mockUserManager = new Mock<UserManager<AppUser>>
                (new Mock<IUserStore<AppUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<AppUser>>().Object,
                new IUserValidator<AppUser>[0],
                new IPasswordValidator<AppUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<AppUser>>>().Object);

            DataSeeder.SeedUsers(_mockUserManager.Object, _context, _mockCurrentUser);
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(_context);
        }
    }
}