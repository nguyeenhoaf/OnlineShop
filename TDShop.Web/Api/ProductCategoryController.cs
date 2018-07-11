using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDShop.Service;
using TDShop.Web.Infrastructure.Core;
using TDShop.Web.Mappings;
using TDShop.Web.Models;

namespace TDShop.Web.Api
{
    [RoutePrefix("API/ProductCategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService) : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _productCategoryService.GetAll();
                var responeData = AutoMapperConfiguration.Mapper.Map<List<ProductCategoryViewModel>>(model);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responeData);

                return response;
            });
        }
    }
}