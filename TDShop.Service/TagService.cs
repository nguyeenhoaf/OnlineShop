using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDShop.Data.Infrastructure;
using TDShop.Data.Repositories;
using TDShop.Model.Models;

namespace TDShop.Service
{
    public interface ITagService
    {
        Tag GetbyId(string tagid);
        void SaveChanges();
    }
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;
        IUnitofWork _unitofWork;
        public TagService(ITagRepository tagRepository,IUnitofWork unitofWork)
        {
            this._tagRepository = tagRepository;
            this._unitofWork = unitofWork;
        }
        public Tag GetbyId(string tagid)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagid);
        }
        public void SaveChanges()
        {
            _unitofWork.Commit();
        }
    }
}
