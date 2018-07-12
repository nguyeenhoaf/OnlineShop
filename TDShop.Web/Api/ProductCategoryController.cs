using System;
using System.Collections.Generic;
using System.Linq;
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
        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _productCategoryService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x=>x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responeData = AutoMapperConfiguration.Mapper.Map<List<ProductCategoryViewModel>>(query);

                PaginationSet<ProductCategoryViewModel> paginationSet = new PaginationSet<ProductCategoryViewModel>()
                {
                    Items = responeData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }
    }
}