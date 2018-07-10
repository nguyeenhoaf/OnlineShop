
using System.Collections.Generic;
using TDShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Linq;
namespace TDShop.Data.Repositories
{
    public interface IPostRepository:IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pc in DbContext.PostCategories
                        on p.CategoryID equals pc.ID
                        where pc.ID == categoryId
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }

        public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}