using Backend.TechChallenge.Api.Base;
using Backend.TechChallenge.Application.Interfaces.Base;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.TechChallenge.Api.Controllers
{
    [Produces("application/json", Type = typeof(UnitOfWorkResult))]
    [Route("[controller]")]
    [ApiController]

    public class UserController : ControllerBase<UserModel, User>
    {
        #region Constant
        #endregion Constant

        #region Variable
        private readonly IService<UserModel, User> _service;
        #endregion Variable

        #region Constructor
        public UserController(IService<UserModel, User> service)
            : base(service)
        {
            _service = service;
        }
        #endregion Constructor

        #region Method
        #endregion Method
    }
}
