using System.Text.Json.Serialization;

namespace PharmacyAppExam.WebApi.ViewModels.Drugs
{
    public class DrugViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = String.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = String.Empty;

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = String.Empty;

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
