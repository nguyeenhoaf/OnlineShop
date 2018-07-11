using TDShop.Data.Infrastructure;
using TDShop.Model.Models;

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