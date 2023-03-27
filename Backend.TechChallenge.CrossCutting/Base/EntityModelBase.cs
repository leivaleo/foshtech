namespace Backend.TechChallenge.CrossCutting.Base
{
    public class EntityModelBase
    {
        public Guid? Guid { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
