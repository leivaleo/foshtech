using AutoMapper;
using Backend.TechChallenge.CrossCutting.Base;
using Backend.TechChallenge.CrossCutting.Enums;
using Backend.TechChallenge.Infrastructure.Interfaces.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.TechChallenge.Infrastructure.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly TechCallengeDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;

        public Repository(TechCallengeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();

            _mapper = mapper;
        }

        #region GET
        public async Task<QueryResult<TEntity>?> GetAll(QueryState<TEntity> queryState, bool? allowTracking = true)
        {
            try
            {
                if (queryState.Take == null || queryState.Take == 0)
                    queryState.Take = 10;

                if (queryState.Skip == null)
                    queryState.Skip = 0;

                IQueryable<TEntity> list = _dbSet;

                // Configure tracking
                if (allowTracking != null && !(bool)allowTracking)
                    list = list.AsNoTracking();

                // Add filtering
                if (queryState.Filter != null)
                    list = list.Where(queryState.Filter);

                // Add sorting
                if (queryState.Sort != null)
                {
                    if (queryState.Sort.Direction == SortOrderEnum.Ascending)
                        list = list.OrderBy(queryState.Sort.By);
                    else
                        list = list.OrderByDescending(queryState.Sort.By);
                }

                // Get total records of the query (without pagination yet)
                var total = list.Count();

                // Add paging
                list = list.Skip((int)queryState.Skip);
                list = list.Take((int)queryState.Take);

                // Add navigable entities
                if (queryState.Include != null)
                {
                    foreach (var includeAttributeName in queryState.Include)
                    {
                        list = list.Include(includeAttributeName);
                    }
                }

                // execute query and prepare the result
                var result = new QueryResult<TEntity>()
                {
                    List = await list.ToListAsync(),
                    Total = total
                };
                return result;
            }
            catch (Exception error)
            {
                // TODO: Log error
            }
            return null;
        }

        public async Task<TEntity?> GetByGuid(Guid guid)
        {
            try
            {
                return await _dbSet.FindAsync(guid);
            }
            catch (Exception error)
            {
                // TODO: Log error
            }
            return null;
        }
        #endregion

        #region INSERT

        public async Task<TEntity?> Insert(TEntity entity)
        {
            try
            {
                var result = await _dbSet.AddAsync(entity);
                return _mapper.Map<TEntity>(result.Entity);

            }
            catch (Exception error)
            {
                // TODO: Log error
            }
            return null;
        }

        #endregion

        #region UPDATE

        public async Task<TEntity?> Update(TEntity entity)
        {
            try
            {
                var result = _dbSet.Update(entity);
                return _mapper.Map<TEntity>(result.Entity);
            }
            catch (Exception error)
            {
                // TODO: Log error
            }
            return null;
        }

        #endregion

        #region DELETE

        public async Task<TEntity?> Delete(TEntity entity)
        {
            try
            {
                bool tracking = _dbContext.ChangeTracker.Entries<TEntity>().Any(x => x.Entity.Guid == entity.Guid);
                if (!tracking)
                {
                    var result = _dbSet.Update(entity);
                    return _mapper.Map<TEntity>(result.Entity);
                }

                return _mapper.Map<TEntity>(entity);
            }
            catch (Exception error)
            {
                // TODO: Log error
            }

            return null;
        }

        public async Task<TEntity?> Undelete(TEntity entity)
        {
            try
            {
                var result = _dbSet.Update(entity);
                return _mapper.Map<TEntity>(result.Entity);
            }
            catch (Exception error)
            {
                // TODO: Log error
            }
            return null;
        }

        #endregion
    }
}
