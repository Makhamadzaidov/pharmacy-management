using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PharmacyAppExam.WebApi.ViewModels.Drugs
{
    public class DrugCreateViewModel
    {
        [Required(ErrorMessage = "Drug name is required field")]
        [MaxLength(35), MinLength(2)]
        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;

        [Required(ErrorMessage = "Description is required field")]
        [JsonPropertyName("description")]
        public string Description { get; set; } = String.Empty;


        [Required(ErrorMessage = "ImageUrl is required field")]
        [JsonPropertyName("imageurl")]
        public string ImageUrl { get; set; } = String.Empty;


        [Required(ErrorMessage = "Price is required field")]
        [JsonPropertyName("price")]
        public int Price { get; set; }


        [Required(ErrorMessage = "Quantity is required field")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
