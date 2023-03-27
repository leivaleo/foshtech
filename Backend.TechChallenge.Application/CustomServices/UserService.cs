using AutoMapper;
using Backend.TechChallenge.Application.Base;
using Backend.TechChallenge.Application.Interfaces.CustomServices;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
using Backend.TechChallenge.CrossCutting.Base;
using Backend.TechChallenge.CrossCutting.Enums;
using Backend.TechChallenge.Infrastructure.Interfaces.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;

namespace Backend.TechChallenge.Application.CustomServices
{
    public class UserService
       : Service<UserModel, User>, IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;


        public UserService(
            IUnitOfWork unitOfWork, 
            IRepository<User> repository, 
            IMapper mapper)
            : base(unitOfWork, repository, mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<UserModel> Insert(UserModel entityModel)
        {
            var updatedUserMoney = entityModel.Money;

            switch (entityModel.UserType)
            {
                case UserTypeEnum.Normal:
                    {
                        if (entityModel.Money >= 100)
                            updatedUserMoney = ApplyPercetangeToDecimal(entityModel.Money, Convert.ToDecimal(0.12)); 

                        else if (entityModel.Money >= 10)
                            updatedUserMoney = ApplyPercetangeToDecimal(entityModel.Money, Convert.ToDecimal(0.8));
                        break;
                    }
                case UserTypeEnum.SuperUser:
                    {
                        updatedUserMoney = UpdateUserMoneyByMoneyLimitToConsidere(entityModel, 100, 0, Convert.ToDecimal(0.2));
                        break;
                    }
                case UserTypeEnum.Premium:
                    {
                        updatedUserMoney = UpdateUserMoneyByMoneyLimitToConsidere(entityModel, 100, 0, 2);
                        break;
                    }
            }
            // Update the money taking into account the corresponding percentage 
            entityModel.Money = updatedUserMoney;

            return base.Insert(entityModel); 
        }

        public async Task<bool> IsDuplicated(UserModel entityModel)
        {
            // Just to demostrate how we use queryState
            var queryState = new QueryState<User>
            {
                Filter = (p => p.Name.Equals(entityModel.Name) || 
                                p.Email.Equals(entityModel.Email) ||
                                p.Address.Equals(entityModel.Address) ||
                                p.Phone.Equals(entityModel.Phone))
            };

            var users = await _repository.GetAll(queryState, false);

            return (users != null && users.Total > 0);
        }

        private decimal UpdateUserMoneyByMoneyLimitToConsidere(UserModel user, decimal moneyLimitToConsidere, decimal lowPercetageToApply = 0, decimal highLimitToApply = 0)
        {
            var percentageToApply = user.Money < moneyLimitToConsidere ? lowPercetageToApply : highLimitToApply;

            return ApplyPercetangeToDecimal(user.Money, percentageToApply);
        }

        private decimal ApplyPercetangeToDecimal(decimal amount, decimal percentage)
        {
            return amount + amount * percentage;
        }
    }
}
