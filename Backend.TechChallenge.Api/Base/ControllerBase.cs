using Backend.TechChallenge.Application.Interfaces.Base;
using Backend.TechChallenge.CrossCutting.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Backend.TechChallenge.Api.Base
{
    public class ControllerBase<TEntityModel, TEntity> : ControllerBase, IControllerBase<TEntityModel>
        where TEntityModel : EntityModelBase
        where TEntity : EntityBase
    {
        #region Constant
        protected String COMBO_TEXT_FIELD_NAME = "";
        #endregion Constant

        #region Variable
        private readonly IService<TEntityModel, TEntity> _service;
        #endregion Variable

        #region Constructor
        public ControllerBase(IService<TEntityModel, TEntity> service)
        {
            _service = service;
        }
        #endregion Constructor

        #region Method
        #region GET
        /// <summary>
        /// Get async data from entity 
        /// </summary>
        /// <returns>All data entity from database</returns>
        /// <response code="201">Returns all entities</response>
        /// <response code="400">If result is null</response>        
        /// <response code="401">If it's unauthorized</response>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<UnitOfWorkResult>> GetAll()
        {
            try
            {
                // This queryState could be received from thr FrontEnd
                var queryState = new QueryState<TEntity>();

                var data = await _service.GetAll(queryState);
                var result = UnitOfWorkResult.SetResultDataOk(data);
                return Ok(result);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }

        }

        /// <summary>
        /// Get one item by Id
        /// </summary>
        /// <param name="guid">Guid to find</param>
        /// <returns>Entity with the Id searched or null</returns>
        /// <response code="201">Returns result data</response>
        /// <response code="400">If result is null</response>        
        /// <response code="401">If it's unauthorized</response>   
        [HttpGet]
        [Route("{guid}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<string>> GetByGuid(Guid guid)
        {
            try
            {
                var data = await _service.GetByGuid(guid);
                var result = UnitOfWorkResult.SetResultDataOk(data);
                return Ok(result);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        #endregion GET

        #region POST
        /// <summary>
        /// Insert new element to the database
        /// </summary>
        /// <typeparam name="TEntityModel"></typeparam>
        /// <param name="entity">Entity data to be inserted</param>
        /// <returns>Added entity</returns>
        /// <response code="201">Returns result data</response>
        /// <response code="400">If result is null</response>        
        /// <response code="401">If it's unauthorized</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<UnitOfWorkResult>> Insert(TEntityModel entityModel)
        {
            try
            {
                var entity = await _service.Insert(entityModel);
                var result = UnitOfWorkResult.SetResultDataOk(entity);

                return Ok(result);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        #endregion POST

        #region PUT
        /// <summary>
        /// Update an element to the database
        /// </summary>
        /// <typeparam name="TEntityModel"></typeparam>
        /// <param name="entityModel">Entity data to be updated</param>
        /// <returns>Updated entity</returns>
        /// <response code="201">Returns result data</response>
        /// <response code="400">If result is null</response>        
        /// <response code="401">If it's unauthorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<UnitOfWorkResult>> Update(TEntityModel entityModel)
        {
            try
            {
                var entity = await _service.Update(entityModel);
                var result = UnitOfWorkResult.SetResultDataOk(entity);

                return Ok(result);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        #endregion PUT

        #region DELETE
        /// <summary>
        /// Delete an element of the database
        /// </summary>
        /// <typeparam name="TEntityModel"></typeparam>
        /// <param name="entityModel">Entity data to be deleted</param>
        /// <returns>Deleted entity</returns>
        /// <response code="201">Returns result data</response>
        /// <response code="400">If result is null</response>        
        /// <response code="401">If it's unauthorized</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<UnitOfWorkResult>> Delete(TEntityModel entityModel)
        {
            try
            {
                var entity = await _service.Delete(entityModel);
                var result = UnitOfWorkResult.SetResultDataOk(entity);

                return Ok(result);
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Undelete an element of the database
        /// </summary>
        /// <typeparam name="TEntityModel"></typeparam>
        /// <param name="entityModel">Entity data to be undeleted</param>
        /// <returns>Undeleted entity</returns>
        /// <response code="201">Returns result data</response>
        /// <response code="400">If result is null</response>        
        /// <response code="401">If it's unauthorized</response>
        [HttpPost]
        [Route("Undelete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public virtual async Task<ActionResult<UnitOfWorkResult>> Undelete(TEntityModel entityModel)
        {
            try
            {
                var entity = await _service.Undelete(entityModel);

                var list = new List<TEntityModel>() { entity };
                var result = UnitOfWorkResult.SetResultDataOk(list);

                return Ok(result); ;
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
        #endregion DELETE
        #endregion Method
    }
}
