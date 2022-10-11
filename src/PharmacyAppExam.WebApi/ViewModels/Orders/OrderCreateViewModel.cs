using PharmacyAppExam.WebApi.Enums;

namespace PharmacyAppExam.WebApi.ViewModels.Orders
{
    public class OrderCreateViewModel
    {
        public long DrugId { get; set; }

        public int Quantity { get; set; }

        public string? CardNumber { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}
