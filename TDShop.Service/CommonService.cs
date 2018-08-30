using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDShop.Common;
using TDShop.Data.Infrastructure;
using TDShop.Data.Repositories;
using TDShop.Model.Models;

namespace TDShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
    }
    public class CommonService : ICommonService
    {
        public IFooterRepository _footerRepository;
        public ISlideRepository _slideRepository;
        public IUnitofWork _unitofWork;
        public CommonService(IFooterRepository footerRepository, ISlideRepository slideRepository, IUnitofWork unitofWork)
        {
            this._footerRepository = footerRepository;
            this._slideRepository = slideRepository;
            this._unitofWork = unitofWork;
        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status == true);
        }
    }
}
