namespace PharmacyAppExam.WebApi.Helpers
{
    public class FileSizeHelper
    {
        public static double ByteToMB(double @byte) => @byte / 1024 / 1024;
    }
}
