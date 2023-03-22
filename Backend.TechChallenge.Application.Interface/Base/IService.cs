using Backend.TechChallenge.CrossCutting.Base;

namespace Backend.TechChallenge.Application.Interfaces.Base
{
    public interface IService<TEntityModel, TEntity>
        where TEntityModel : EntityModelBase
        where TEntity : EntityBase
    {
        Task<(List<TEntityModel>, int)> GetAll(QueryState<TEntity> queryState);
        Task<TEntityModel> GetByGuid(Guid guid);

        Task<TEntityModel> Insert(TEntityModel entityModel);
        Task<TEntityModel> Update(TEntityModel entityModel);
        Task<TEntityModel> Delete(TEntityModel entityModel);
        Task<TEntityModel> Undelete(TEntityModel entityModel);
    }
}
