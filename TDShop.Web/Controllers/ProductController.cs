using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TDShop.Common;
using TDShop.Model.Models;
using TDShop.Service;
using TDShop.Web.Infrastructure.Core;
using TDShop.Web.Mappings;
using TDShop.Web.Models;

namespace TDShop.Web.Controllers
{
    public class ProductController : Controller
    {
        public IProductService _productService;
        public IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category( int id, int page = 1, string sortType="")
        {
            int pageSize = Convert.ToInt32(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var model = _productService.GetProductsByCategoryIdPaging(id, page, pageSize, out totalRow, sortType);
            var category = _productCategoryService.GetById(id);
            var categoryViewModel = AutoMapperConfiguration.Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            ViewBag.Category = categoryViewModel;
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            int maxPage = Convert.ToInt32(ConfigHelper.GetByKey("MaxPage"));
            var productViewModel = AutoMapperConfiguration.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                TotalCount = totalRow,
                Items = productViewModel,
                TotalPages = totalPage,
                MaxPage = maxPage,
                Page = page
            };
            
            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetProductsByName(keyword);
            var a = model.Count();
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, string sortType = "")
        {
            int pageSize = Convert.ToInt32(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var model = _productService.Search(keyword, page, pageSize, out totalRow, sortType);

            ViewBag.Keyword = keyword;
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            int maxPage = Convert.ToInt32(ConfigHelper.GetByKey("MaxPage"));
            var productViewModel = AutoMapperConfiguration.Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                TotalCount = totalRow,
                Items = productViewModel,
                TotalPages = totalPage,
                MaxPage = maxPage,
                Page = page
            };

            return View(paginationSet);
        }
    }
}
