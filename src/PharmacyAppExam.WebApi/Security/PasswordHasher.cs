namespace PharmacyAppExam.WebApi.Security
{
    public class PasswordHasher
    {
        private static string _key = "7fbd7843-a7e8-4f18-8f99-74cd4dba3039";

        public static (string Hash, string Salt) Hash(string password)
        {
            string salt = Guid.NewGuid().ToString();
            string hash = BCrypt.Net.BCrypt.HashPassword(salt + password + _key);
            return (hash, salt);
        }
        public static bool Verify(string password, string salt, string hash)
            => BCrypt.Net.BCrypt.Verify(salt + password + _key, hash);
    }
}
