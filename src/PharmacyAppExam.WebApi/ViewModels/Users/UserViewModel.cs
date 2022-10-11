namespace PharmacyAppExam.WebApi.ViewModels.Users
{
    public class UserViewModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
