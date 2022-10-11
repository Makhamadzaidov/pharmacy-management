namespace PharmacyAppExam.WebApi.ViewModels.Drugs
{
    public class DrugViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
