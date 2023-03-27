using AutoMapper;
using Backend.TechChallenge.Api.Config.Mapper;
using Backend.TechChallenge.Application.CustomServices;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
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
    public class UserServiceUnitTest
    {
        private static IMapper _mapper;

        public UserServiceUnitTest() 
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        #region GET
        [Fact]
        public async Task It_should_get_all_user_entities_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);
            
            var sut = new UserService(unitOfWork, repository, _mapper);

            //Act
            var response = await sut.GetAll(new QueryState<User>());
            
            var list = response.Item1;
            var total = response.Item2;

            //Assert
            Assert.NotNull(list);
            Assert.Equal(list.Count, total);

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_get_user_by_guid_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);
            var _guid = new Guid("00000000-0000-0000-0000-000000000002");

            var sut = new UserService(unitOfWork, repository, _mapper);
            
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

        #region NORMAL_USER
        [Fact]
        public async Task It_should_insert_normal_user_money_more_than_100_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(0.12);
            var _money = 1000;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Normal,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));
                
            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_insert_normal_user_money_more_than_10_and_less_than_100_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(0.8);
            var _money = 50;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Normal,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_insert_normal_user_money_less_than_10_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(0.0);
            var _money = 8;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Normal,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));

            //Clean up
            await dbContext.DisposeAsync();
        }
        #endregion NORMAL_USER

        #region SUPER_USER
        [Fact]
        public async Task It_should_insert_super_user_money_more_than_100_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(0.2);
            var _money = 1000;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.SuperUser,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_insert_super_user_money_less_than_100_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(0.0);
            var _money = 50;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.SuperUser,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));

            //Clean up
            await dbContext.DisposeAsync();
        }
        #endregion SUPER_USER

        #region PREMIUM_USER
        [Fact]
        public async Task It_should_insert_premium_user_money_more_than_100_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(2);
            var _money = 1000;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Premium,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_insert_premium_user_money_less_than_100_successfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);

            var _guid = new Guid("00000000-0000-0000-0000-000000000010");
            var _percentage = Convert.ToDecimal(0.0);
            var _money = 50;

            var sut = new UserService(unitOfWork, repository, _mapper);

            var user = new UserModel
            {
                Guid = _guid,
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Premium,
                Money = _money
            };

            //Act
            var entity = await sut.Insert(user);

            //Assert
            Assert.NotNull(entity);
            Assert.Equal(entity.Guid, _guid);
            Assert.Equal(entity.Money, (_money + _money * _percentage));

            //Clean up
            await dbContext.DisposeAsync();
        }
        #endregion PREMIUM_USER

        #endregion INSERT
    }
}
