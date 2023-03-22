using Backend.TechChallenge.CrossCutting.Base;

namespace Backend.TechChallenge.Infrastructure.Interfaces.Base
{
    public interface IRepository { }

    public interface IRepository<TEntity> : IRepository where TEntity : EntityBase
    {
        Task<QueryResult<TEntity>> GetAll(QueryState<TEntity> queryState, bool allowTracking);
        Task<TEntity> GetByGuid(Guid guid, bool allowTracking);

        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity> Undelete(TEntity entity);
    }
}
