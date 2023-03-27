using Backend.TechChallenge.Application.Interfaces.Base;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;

namespace Backend.TechChallenge.Application.Interfaces.CustomServices
{
    public interface IUserService
        : IService<UserModel, User>
    {
        Task<bool> IsDuplicated(UserModel entityModel);
    }
}
