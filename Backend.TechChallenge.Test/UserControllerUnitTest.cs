using AutoMapper;
using Backend.TechChallenge.Api.Base;
using Backend.TechChallenge.Api.Config.Mapper;
using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Application.CustomServices;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
using Backend.TechChallenge.CrossCutting.Enums;
using Backend.TechChallenge.Infrastructure.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Backend.TechChallenge.TestHelpers;
using Backend.TechChanllenge.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Backend.TechChallenge.Test
{
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]

    [Collection("Non-Parallel Collection")]
    public class UserControllerUnitTest
    {
        private static IMapper _mapper;

        public UserControllerUnitTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMapping());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        #region INSERT
        [Fact]
        public async Task It_should_insert_user_with_incomplete_data_fails()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);
            var service = new UserService(unitOfWork, repository, _mapper);

            var sut = new UserController(service);

            var user = new UserModel
            {
                Name = "",
                Address = "",
                Email = "",
                Phone = "",
                UserType = UserTypeEnum.Normal,
                Money = 100
            };

            //Act
            var actionResult = await sut.Insert(user);
            
            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            
            var unitOfWorkResult = ObjectHelpes.GetObjectResultContent<UnitOfWorkResult>(actionResult);
            Assert.NotNull(unitOfWorkResult);
            Assert.False(unitOfWorkResult.StatusOk);
            Assert.Equal(0, unitOfWorkResult.ProcessOk);
            Assert.NotNull(unitOfWorkResult.Error);
            Assert.Contains("The name is required", unitOfWorkResult.Error.Message);
            Assert.Contains("The email is required", unitOfWorkResult.Error.Message);
            Assert.Contains("The address is required", unitOfWorkResult.Error.Message);
            Assert.Contains("The phone is required", unitOfWorkResult.Error.Message);

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_insert_user_is_duplicated_fails()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);
            var service = new UserService(unitOfWork, repository, _mapper);

            var sut = new UserController(service);

            var user = new UserModel
            {
                Name = "Normal user to test",
                Address = "Address of normal user",
                Email = "Email of normal user",
                Phone = "11111111",
                UserType = UserTypeEnum.Normal,
                Money = 100
            };

            //Act
            var actionResult = await sut.Insert(user);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            var unitOfWorkResult = ObjectHelpes.GetObjectResultContent<UnitOfWorkResult>(actionResult);
            Assert.NotNull(unitOfWorkResult);
            Assert.False(unitOfWorkResult.StatusOk);
            Assert.Equal(0, unitOfWorkResult.ProcessOk);
            Assert.NotNull(unitOfWorkResult.Error);
            Assert.Contains("User is duplicated", unitOfWorkResult.Error.Message);

            //Clean up
            await dbContext.DisposeAsync();
        }

        [Fact]
        public async Task It_should_insert_user_sucessfully()
        {
            //Arrange
            var dbContext = DatabaseInMemoryHelper.CreateDbContext();
            var unitOfWork = new UnitOfWork<TechCallengeDbContext>(dbContext);
            var repository = new Repository<User>(dbContext, _mapper);
            var service = new UserService(unitOfWork, repository, _mapper);

            var _percentage = Convert.ToDecimal(0.12);
            var _money = 1000;

            var sut = new UserController(service);

            var user = new UserModel
            {
                Name = "New user to test",
                Address = "Address of newUser",
                Email = "Email of newUser",
                Phone = "44444444",
                UserType = UserTypeEnum.Normal,
                Money = _money
            };

            //Act
            var actionResult = await sut.Insert(user);

            //Assert
            Assert.NotNull(actionResult);
            Assert.IsType<OkObjectResult>(actionResult.Result);

            var unitOfWorkResult = ObjectHelpes.GetObjectResultContent<UnitOfWorkResult>(actionResult);
            Assert.NotNull(unitOfWorkResult);
            Assert.True(unitOfWorkResult.StatusOk);
            Assert.Equal(1, unitOfWorkResult.ProcessOk);
            Assert.Null(unitOfWorkResult.Error);
            Assert.NotNull(unitOfWorkResult.Data);
            Assert.Equal(1, unitOfWorkResult.Total);

            var insertedUser = (UserModel)unitOfWorkResult.Data[0];
            Assert.Equal(user.Name, insertedUser.Name);
            Assert.Equal(user.Address, insertedUser.Address);
            Assert.Equal(user.Email, insertedUser.Email);
            Assert.Equal(user.Address, insertedUser.Address);
            Assert.Equal(user.Phone, insertedUser.Phone);
            Assert.Equal(_money + _money * _percentage, insertedUser.Money);


            //Clean up
            await dbContext.DisposeAsync();
        }
        #endregion INSERT

    }
}
