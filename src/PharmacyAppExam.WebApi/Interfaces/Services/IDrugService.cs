using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.ViewModels.Drugs;
using System.Linq.Expressions;

namespace PharmacyAppExam.WebApi.Interfaces.Services
{
    public interface IDrugService
    {
        Task<DrugViewModel> CreateAsync(DrugCreateViewModel drugCreate);
        Task<bool> UpdateAsync(long id, DrugCreateViewModel drugUpdate);
        Task<bool> DeleteAsync(Expression<Func<Drug, bool>> expression);
        Task<DrugViewModel?> GetAsync(Expression<Func<Drug, bool>> expression);
        Task<IEnumerable<DrugViewModel>> GetAllAsync(Expression<Func<Drug, bool>>? expression = null,
            PaginationParams? @params = null);
    }
}
