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
    public interface IPageService
    {
        Page GetPagebyAlias(string alias);
    }
    public class PageService : IPageService
    {
        public IPageRepository _pageRepository;
        public IUnitofWork _unitofWork;
        public PageService(IPageRepository pageRepository, IUnitofWork unitofWork)
        {
            this._pageRepository = pageRepository;
            this._unitofWork = unitofWork;
        }
        public Page GetPagebyAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias && x.Status);
        }
    }
}
