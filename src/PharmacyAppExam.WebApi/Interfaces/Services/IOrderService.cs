using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.ViewModels.Orders;
using System.Linq.Expressions;

namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateAsync(long userid, OrderCreateViewModel orderCreateViewModel);
        Task<bool> UpdateAsync(long Id, OrderCreateViewModel orderCreateViewModel);
        Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression);
        Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression);
        Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
            PaginationParams? @params = null);
    }
}
