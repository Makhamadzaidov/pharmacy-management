namespace PharmacyAppExam.WebApi.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor = null!;
        public static HttpResponse Response => Accessor.HttpContext.Response;
        public static HttpRequest Request => Accessor.HttpContext.Request;
        public static IHeaderDictionary ResponseHeaders => Response.Headers;
        public static IHeaderDictionary RequestHeaders => Response.Headers;
        public static int UserId => int.Parse(Accessor.HttpContext.User.FindFirst("Id")?.Value ?? "0");
    }
}
