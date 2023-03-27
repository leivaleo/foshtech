using AutoMapper;
using Backend.TechChallenge.Api.Config.Mapper;
using Backend.TechChallenge.CrossCutting.Base;
using Backend.TechChallenge.CrossCutting.Enums;
using Backend.TechChallenge.Infrastructure.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Backend.TechChanllenge.TestHelpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Backend.TechChallenge.Test
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]

    [Collection("Non-Parallel Collection")]
    public class RepositoryUnitTest
    {
        private static IMapper _mapper;

        public RepositoryUnitTest() 
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ApplicationMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        #region GET
        [Fact]
        public async Task It_should_get_all_user_entities_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var sut = new Repository<User>(dbContext, _mapper);

            //Act
            var entity = await sut.GetAll(new QueryState<User>());

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(3, entity.Total);

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_get_user_by_guid_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var sut = new Repository<User>(dbContext, _mapper);
            var _guid = new Guid("00000000-0000-0000-0000-000000000002");

            //Act
            var entity = await sut.GetByGuid(_guid);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);

            //Clean up
            await dbContext.DisposeAsync();
        }
        #endregion GET

        #region INSERT
        [Fact]
        public async Task It_should_insert_user_entity_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var sut = new Repository<User>(dbContext, _mapper);
            var _guid = new Guid("00000000-0000-0000-0000-000000000011");

            var user = new User
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Normal,
                Money = 50
            };

            //Act
            var operationResult = await sut.Insert(user);
            var entityFromDB = await sut.GetByGuid(_guid);

            //Assert
            Assert.NotNull(operationResult);
            Assert.NotNull(entityFromDB);

            Assert.Equal(operationResult, entityFromDB);

            //Clean up
            await dbContext.DisposeAsync();
        }

        #endregion INSERT
    }
}
