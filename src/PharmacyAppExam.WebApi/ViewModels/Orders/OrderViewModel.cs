namespace PharmacyAppExam.WebApi.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string DrugName { get; set; } = String.Empty;

        public string UserFullName { get; set; } = String.Empty;

        public int Quantity { get; set; }

        public int TotalSum { get; set; }

        public string PaymentType { get; set; } = String.Empty;

        public string CardNumber { get; set; } = String.Empty;
    }
}
