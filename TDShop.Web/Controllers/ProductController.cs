using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace TDShop.Web.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category( int id)
        {
            return View();
        }
    }
}
