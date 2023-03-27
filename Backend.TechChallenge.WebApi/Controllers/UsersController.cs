using Backend.TechChallenge.Api.Base;
using Backend.TechChallenge.Application.Interfaces.CustomServices;
using Backend.TechChallenge.Application.Interfaces.EntityModels.User;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Backend.TechChallenge.Api.Controllers
{
    [Produces("application/json", Type = typeof(UnitOfWorkResult))]
    [ApiController]
    [Route("[controller]")]
    
    public class UserController : ControllerBase<UserModel, User>
    {
        #region Constant
        #endregion Constant

        #region Variable
        private readonly IUserService _service;
        #endregion Variable

        #region Constructor
        public UserController(IUserService service)
            : base(service)
        {
            _service = service;
        }
        #endregion Constructor

        #region Method
        public override async Task<ActionResult<UnitOfWorkResult>> Insert(UserModel entityModel)
        {
            if (entityModel == null)
            {
                return BadRequest(UnitOfWorkResult.SetResultError("User data is null"));
            }

            // Validate data
            var validationErrors = UserModel.ValidateErrors(entityModel);
            if (!String.IsNullOrEmpty(validationErrors))
            {
                return BadRequest(UnitOfWorkResult.SetResultError(validationErrors));
            }

            // Validate if user allreday exists
            if (await _service.IsDuplicated(entityModel))
            {
                return BadRequest(UnitOfWorkResult.SetResultError("User is duplicated"));
            }

            try
            {
                return await base.Insert(entityModel);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        #endregion Method
    }
}
