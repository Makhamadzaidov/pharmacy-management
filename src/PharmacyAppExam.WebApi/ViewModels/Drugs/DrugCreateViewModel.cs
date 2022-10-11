using System.ComponentModel.DataAnnotations;

namespace PharmacyAppExam.WebApi.ViewModels.Drugs
{
    public class DrugCreateViewModel
    {
        [Required(ErrorMessage = "Drug name is required")]
        [MaxLength(35), MinLength(2)]
        public string Name { get; set; } = String.Empty;


        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }


        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
