using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Viseo.Application.Common.Interfaces;
using Viseo.Application.Common.Mapping;
using Viseo.Domain.Entities;
using Viseo.Persistence;
//using Viseo.Persistence.JwtConfig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Viseo.Application.Common.Configuration;

namespace Viseo.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ApplicationDbContext Context { get; private set; } 
        public IMapper Mapper { get; private set; }
        public Mock<IJwtAuthManager> _mockjwtAuthManager  { get; private set; }
        public Mock<ICurrentUserService> MockCurrentUser { get; private set; }
        public Mock<UserManager<AppUser>> _mockUserManager  { get; private set; }
  
        public QueryTestFixture()
        {
            MockCurrentUser = new Mock<ICurrentUserService>();
            _mockjwtAuthManager = new Mock<IJwtAuthManager>();
           // MockDateTime = new Mock<IDateTime>();
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


            Context = DbContextFactory.Create(MockCurrentUser.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AllowNullDestinationValues = false;
            });

            Mapper = configurationProvider.CreateMapper();
            DataSeeder.SeedUsers(_mockUserManager.Object, Context, MockCurrentUser);
        }

        public void Dispose()
        {
            DbContextFactory.Destroy(Context);
        }
    }
}