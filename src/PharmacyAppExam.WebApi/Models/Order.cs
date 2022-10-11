using PharmacyAppExam.WebApi.Commons;
using PharmacyAppExam.WebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAppExam.WebApi.Models
{
    public class Order : Auditable
    {
        public long DrugId { get; set; }

        public virtual Drug Drug { get; set; } = null!;

        public long UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public int Quantity { get; set; }

        public PaymentType PaymentType { get; set; }

        [MaxLength(19)]
        public string? CardNumber { get; set; }
    }
}
