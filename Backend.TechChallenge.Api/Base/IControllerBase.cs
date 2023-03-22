using Backend.TechChallenge.CrossCutting.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Backend.TechChallenge.Api.Base
{
    public interface IControllerBase<TModel> where TModel : EntityModelBase
    {
        Task<ActionResult<UnitOfWorkResult>> GetAll();
        Task<ActionResult<string>> GetByGuid(Guid guid);

        Task<ActionResult<UnitOfWorkResult>> Insert(TModel entity);
        Task<ActionResult<UnitOfWorkResult>> Update(TModel entity);
        Task<ActionResult<UnitOfWorkResult>> Delete(TModel entity);
        Task<ActionResult<UnitOfWorkResult>> Undelete(TModel entity);
    }
}
