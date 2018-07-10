using TDShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TDShop.Data.Repositories
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}