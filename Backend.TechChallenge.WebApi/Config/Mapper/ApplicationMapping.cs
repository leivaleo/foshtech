using AutoMapper;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;

namespace Backend.TechChallenge.Api.Config.Mapper
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            #region User
            CreateMap<UserModel, User>().ReverseMap();
            #endregion User
        }
    }
}
