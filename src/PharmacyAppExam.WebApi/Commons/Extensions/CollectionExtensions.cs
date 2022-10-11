using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Helpers;

namespace PharmacyAppExam.WebApi.Commons.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IQueryable<TSource> sources,
        PaginationParams? @params)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("total-count"))
                HttpContextHelper.ResponseHeaders.Remove("total-count");

            HttpContextHelper.ResponseHeaders.Add("total-count", $"{sources.Count()}");

            return @params is { PageSize: > 0, PageIndex: > 0 }
                ? sources.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
                : sources;
        }
        public static IQueryable<TSource> ToPagedAsQueryable<TSource>(this IQueryable<TSource> sources,
            PaginationParams? @params)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("total-count"))
                HttpContextHelper.ResponseHeaders.Remove("total-count");

            HttpContextHelper.ResponseHeaders.Add("total-count", $"{sources.Count()}");

            return @params is { PageSize: > 0, PageIndex: > 0 }
                ? sources.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
                : sources;
        }
        public static IEnumerable<TSource> ToPagedAsEnumerable<TSource>(this IEnumerable<TSource> sources,
           PaginationParams? @params)
           => @params is { PageSize: > 0, PageIndex: > 0 }
               ? sources.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
               : sources;
    }
}
