namespace Backend.TechChallenge.CrossCutting.Base
{
    public class QueryResult<TEntity> where TEntity : EntityBase
    {
        public List<TEntity> List { get; set; }
        public int Total { get; set; }
    }
}
