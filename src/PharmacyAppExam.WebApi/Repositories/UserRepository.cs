using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Interfaces.IRepositories;
using PharmacyAppExam.WebApi.Models;
using System.Linq.Expressions;

namespace PharmacyAppExam.WebApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        internal Task GetAsync(Expression<Func<Telegram.Bot.Types.User, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
