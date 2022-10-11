namespace PharmacyAppExam.WebApi.Commons
{
    public abstract class Auditable : BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
