using TDShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TDShop.Data.Repositories
{
    public interface IVisitorStasticRepository : IRepository<VisitorStatistic>
    {

    }
    public class VisitorStasticRepository : RepositoryBase<VisitorStatistic>, IVisitorStasticRepository
    {
        public VisitorStasticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}