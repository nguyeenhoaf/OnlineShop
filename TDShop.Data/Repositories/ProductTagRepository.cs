using TDShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TDShop.Data.Repositories
{
    public interface IProductTagReponsitory : IRepository<ProductTag>
    {
    }

    public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagReponsitory
    {
        public ProductTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}