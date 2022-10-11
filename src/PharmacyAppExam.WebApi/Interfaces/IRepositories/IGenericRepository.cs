using System.Linq.Expressions;

namespace PharmacyAppExam.WebApi.Interfaces.IRepositories
{
    public interface IGenericRepository<TSource> where TSource : class
    {
        Task<TSource> CreateAsync(TSource source);
        Task<TSource> UpdateAsync(TSource source);
        Task DeleteAsync(TSource source);
        Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression);
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>>? expression = null, bool isTracking = true);
    }
}
