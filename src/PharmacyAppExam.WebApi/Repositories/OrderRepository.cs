using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Interfaces.IRepositories;
using PharmacyAppExam.WebApi.Models;

namespace PharmacyAppExam.WebApi.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
