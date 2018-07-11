using TDShop.Data.Infrastructure;
using TDShop.Model.Models;

namespace TDShop.Data.Repositories
{
    public interface IMenuGroupRepository: IRepository<MenuGroup>
    {
    }

    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}