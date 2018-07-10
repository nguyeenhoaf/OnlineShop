using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TDShop.Data.Repositories;
using TDShop.Service;
using TDShop.Web.Infrastructure.Core;
using TDShop.Web.Mappings;
using TDShop.Web.Models;
using TeduShop.Model.Models;

namespace TDShop.Web.API
{
    [RoutePrefix("API/PostCategory")]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        protected HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listCategory = _postCategoryService.GetAll();

                    var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(listCategory);

                    response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVM);

                }
                return response;
            });
        }
        [Route("add")]
        protected HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategory = Mapper.Map<PostCategory>(postCategoryVM);
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.Created, category);

                }
                return response;
            });
        }
        [Route("update")]
        protected HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategory = Mapper.Map<PostCategory>(postCategoryVM);
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;
            });
        }

        protected HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;
            });
        }
       
    }

}