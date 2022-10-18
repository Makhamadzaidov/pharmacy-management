using PharmacyAppExam.WebApi.Commons;
using System.ComponentModel.DataAnnotations;

namespace PharmacyAppExam.WebApi.Models
{
    public class Drug : Auditable
    {
        [MaxLength(35)]
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string ImagePath { get; set; } = String.Empty;

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
