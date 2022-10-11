using PharmacyAppExam.WebApi.Commons.Utils;
using PharmacyAppExam.WebApi.Interfaces.Services;
using PharmacyAppExam.WebApi.Models;
using PharmacyAppExam.WebApi.ViewModels.Users;
using System.Linq.Expressions;


namespace PharmacyAppExam.WebApi.Services
{
    public class UserService : IUserService
    {
        public Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null, PaginationParams? @params = null)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> UpdateAsync(long id, UserCreateViewModel userCreateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
