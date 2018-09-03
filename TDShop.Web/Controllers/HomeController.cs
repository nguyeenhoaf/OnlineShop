using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TDShop.Model.Models;
using TDShop.Service;
using TDShop.Web.Mappings;
using TDShop.Web.Models;
namespace TDShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public IProductCategoryService _productCategoryService;
        public IProductService _productService;
        public ICommonService _commonService;
        public HomeController(IProductCategoryService productCategoryService, IProductService productService, ICommonService commonService)
        {
            this._productCategoryService = productCategoryService;
            this._productService = productService;
            this._commonService = commonService;
        }
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideViewModel = AutoMapperConfiguration.Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);

            var lastestProductModel = _productService.GetLastest(3);
            var lastestProductViewModel = AutoMapperConfiguration.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);

            var topSaleProductModel = _productService.GetHotProduct(3);
            var topSaleProductViewModel = AutoMapperConfiguration.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            var homeViewModel = new HomeViewModel();

            homeViewModel.Slides = slideViewModel;
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;
            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = AutoMapperConfiguration.Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }
        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = AutoMapperConfiguration.Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
    }
}