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
    public class PageController : Controller
    {
        public IPageService _pageService;
        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var pageModel = _pageService.GetPagebyAlias(alias);
            var pageViewModel=AutoMapperConfiguration.Mapper.Map<Page, PageViewModel>(pageModel);
            
            return View(pageViewModel);
        }
    }
}