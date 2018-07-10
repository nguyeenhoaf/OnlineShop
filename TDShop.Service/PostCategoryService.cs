using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDShop.Data.Infrastructure;
using TDShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TDShop.Service
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory postCategory);

        void Update(PostCategory postCategory);

        PostCategory Delete(int id);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetAllByParentId(int id);

        PostCategory GetById(int id);

        void SaveChanges();
    }
    public class PostCategoryService:IPostCategoryService
    {
        IPostCategoryRepository _postCategoryRepository;
        IUnitofWork _unitofWork;
        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitofWork unitofWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitofWork = unitofWork;
        }

        public PostCategory Add(PostCategory postCategory)
        {
            return _postCategoryRepository.Add(postCategory);
        }

        public PostCategory Delete(int id)
        {
            return _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int id)
        {
            return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == id);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitofWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }
    }
}
