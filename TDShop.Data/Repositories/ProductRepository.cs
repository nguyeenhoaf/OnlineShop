using TDShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TDShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}