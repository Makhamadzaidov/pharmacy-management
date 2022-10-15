using System.ComponentModel.DataAnnotations;

namespace PharmacyAppExam.WebApi.ViewModels.Users
{
    public class EmailAddress
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; } = String.Empty;
    }
}
