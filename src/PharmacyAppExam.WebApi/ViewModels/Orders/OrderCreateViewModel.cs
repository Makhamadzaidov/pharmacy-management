using PharmacyAppExam.WebApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAppExam.WebApi.ViewModels.Orders
{
    public class OrderCreateViewModel
    {
        [Required(ErrorMessage = "Drug Id is reuqired field")]
        public long DrugId { get; set; }

        [Required(ErrorMessage = "Quantity is required field")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Payment type is required field")]
        public PaymentType PaymentType { get; set; }

        public string? CardNumber { get; set; }
    }
}
