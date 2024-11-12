namespace CompanyPortal.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public virtual bool Status { get; set; } = true;
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
