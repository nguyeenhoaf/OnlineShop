using TDShop.Data.Infrastructure;
using TDShop.Model.Models;

namespace TDShop.Data.Repositories
{
    public interface IFooterRepository:IRepository<Footer>
    {
    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        { }
    }
}