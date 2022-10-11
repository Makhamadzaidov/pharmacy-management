using PharmacyAppExam.WebApi.DbContexts;
using PharmacyAppExam.WebApi.Interfaces.IRepositories;
using PharmacyAppExam.WebApi.Models;

namespace PharmacyAppExam.WebApi.Repositories
{
    public class DrugRepository : GenericRepository<Drug>, IDrugRepository
    {
        public DrugRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
