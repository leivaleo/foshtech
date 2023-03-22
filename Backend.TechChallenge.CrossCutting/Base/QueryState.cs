using Backend.TechChallenge.CrossCutting.Enums;
using System.Linq.Expressions;

namespace Backend.TechChallenge.CrossCutting.Base
{
    public class QueryState<TEntity> where TEntity : EntityBase
    {
        public Expression<Func<TEntity, bool>> Filter { get; set; }
        public QueryStateSort<TEntity> Sort { get; set; }
        public List<Expression<Func<TEntity, object>>> Include { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }

    public class QueryStateSort<TEntity> where TEntity : EntityBase
    {
        public Expression<Func<TEntity, string>> By { get; set; }
        public SortOrderEnum Direction { get; set; }

        public QueryStateSort()
        {
        }

        public QueryStateSort(Expression<Func<TEntity, string>> by, SortOrderEnum direction)
        {
            By = by;
            Direction = direction;
        }
    }
}
