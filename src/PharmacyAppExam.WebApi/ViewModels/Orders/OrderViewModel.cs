namespace PharmacyAppExam.WebApi.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public string DrugName { get; set; } = String.Empty;

        public string UserName { get; set; } = String.Empty;

        public int Quantiry { get; set; }

        public string CardNumber { get; set; } = String.Empty;

        public string PaymentType { get; set; } = String.Empty;
    }
}
