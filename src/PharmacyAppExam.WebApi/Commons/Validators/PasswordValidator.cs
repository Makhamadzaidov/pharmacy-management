namespace PharmacyAppExam.WebApi.Commons.Validators
{
    public class PasswordValidator
    {
        public static (bool IsValid, string Message) IsStrong(string password)
        {
            bool isLower = false;
            bool isUpper = false;
            bool isDigit = false;
            bool isChar = false;

            for (int i = 0; i < password.Length; i++)
            {
                int k = (int)password[i];
                if (k >= 97 && k <= 122) isLower = true;
                else if (k >= 65 && k <= 90) isUpper = true;
                else if (k >= 48 && k <= 57) isDigit = true;
                else if (k > 32 && k < 127) isChar = true;
            }
            if (!isLower)
                return (IsValid: false, Message: "Password must be at least one lower letter");
            if (!isUpper)
                return (IsValid: false, Message: "Password must be at least one upper letter");
            if (!isDigit)
                return (IsValid: false, Message: "Password must be at least one digit");
            if (!isChar)
                return (IsValid: false, Message: "Password must be at least one character as (!@#$%^&*()-+)");

            return (IsValid: true, Message: "Password is strong!");
        }
    }
}
