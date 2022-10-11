namespace PharmacyAppExam.WebApi.Helpers
{
    public class ImageHelper
    {
        public static string MakeImageName(string fileName)
        {
            string guid = Guid.NewGuid().ToString();
            return "IMG_" + guid + fileName;
        }
    }
}
