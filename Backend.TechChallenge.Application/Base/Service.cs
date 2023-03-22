using AutoMapper;
using Backend.TechChallenge.Application.Interfaces.Base;
using Backend.TechChallenge.CrossCutting.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Base;

namespace Backend.TechChallenge.Application.Base
{
    public class Service<TEntityModel, TEntity>
       : IService<TEntityModel, TEntity>
       where TEntityModel : EntityModelBase
       where TEntity : EntityBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Service(
            IUnitOfWork unitOfWork, 
            IRepository<TEntity> repository, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;

        }

        #region CUSTOM
        public IUnitOfWork GetUnitOfWork()
        {
            return _unitOfWork;
        }
        #endregion

        #region GET
        public async virtual Task<(List<TEntityModel>, int)> GetAll(QueryState<TEntity> queryState)
        {
            var queryResult = await _repository.GetAll(queryState, true);
            return (_mapper.Map<List<TEntityModel>>(queryResult.List), queryResult.Total);
        }

        public async virtual Task<TEntityModel> GetByGuid(Guid guid)
        {
            TEntity listEntity = await _repository.GetByGuid(guid, true);
            return _mapper.Map<TEntityModel>(listEntity);
        }
        #endregion GET

        #region POST
        public async virtual Task<TEntityModel> Insert(TEntityModel entityModel)
        {
            TEntity entity = _mapper.Map<TEntity>(entityModel);

            entity.IsDeleted = false;
            entity.InsertedDate = DateTime.UtcNow;

            TEntity result = await _repository.Insert(entity);
            _unitOfWork.SaveChanges();

            return _mapper.Map<TEntityModel>(result);
        }
        #endregion POST

        #region PUT
        public async virtual Task<TEntityModel> Update(TEntityModel entityModel)
        {
            TEntity entity = _mapper.Map<TEntity>(entityModel);

            entity.UpdatedDate = DateTime.UtcNow;

            TEntity result = await _repository.Update(entity);
            _unitOfWork.SaveChanges();

            return _mapper.Map<TEntityModel>(result);
        }
        #endregion PUT

        #region DELETE
        public async virtual Task<TEntityModel> Delete(TEntityModel entityModel)
        {
            TEntity entity = _mapper.Map<TEntity>(entityModel);

            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.UtcNow;

            TEntity result = await _repository.Delete(entity);
            _unitOfWork.SaveChanges();

            return _mapper.Map<TEntityModel>(result);
        }

        public async Task<TEntityModel> Undelete(TEntityModel entityModel)
        {
            TEntity entity = _mapper.Map<TEntity>(entityModel);

            entity.IsDeleted = false;
            entity.UpdatedDate = DateTime.UtcNow;

            TEntity result = await _repository.Undelete(entity);
            _unitOfWork.SaveChanges();

            return _mapper.Map<TEntityModel>(result);
        }
        #endregion DELETE
    }
}
