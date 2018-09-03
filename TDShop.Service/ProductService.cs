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
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        Product GetById(int id);

        IEnumerable<Product> GetRelatedProducts(int id, int top);

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Product> GetProductsByCategoryIdPaging(int CategoryID, int page, int pageSize, out int totalRow, string sortType);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow, string sortType);

        IEnumerable<string> GetProductsByName(string name);

        IEnumerable<Tag> GetListTagByProductId(int productId);

        void IncreaseView(int id);

        IEnumerable<Product> GetListProductByTagId(string tagId, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        IProductTagRepository _productTagRepository;
        ITagRepository _tagRepository;
        IUnitofWork _unitofWork;
        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository, ITagRepository tagRepository, IUnitofWork unitofWork)
        {
            this._productRepository = productRepository;
            this._tagRepository = tagRepository;
            this._productTagRepository = productTagRepository;
            this._unitofWork = unitofWork;
        }

        public Product Add(Product product)
        {
            var Product = _productRepository.Add(product);
            _unitofWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] Tags = product.Tags.Split(',');
                
                for(var i=0; i<Tags.Length; ++i)
                {
                    var tagId = StringHelper.ToUnsignString(Tags[i]);
                    if(_tagRepository.Count(x=>x.ID==tagId)==0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Type = CommonConstants.ProductTag;
                        tag.Name = Tags[i];
                        _tagRepository.Add(tag);                      
                    }
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
                _unitofWork.Commit();
            }
            return Product;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag==true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByTagId(string tagId, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetListProductByTag(tagId, page, pageSize,out totalRow);
        }

        public IEnumerable<Tag> GetListTagByProductId(int productId)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == productId, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public IEnumerable<Product> GetProductsByCategoryIdPaging(int CategoryID, int page, int pageSize, out int totalRow, string sortType)
        {
            
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == CategoryID);
            switch (sortType)
            {
                case "popular": query=query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount": query=query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price": query=query.OrderBy(x => x.Price);
                    break;
                default:
                        query=query.OrderByDescending(x => x.CreatedDate);
                        break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetProductsByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> GetRelatedProducts(int id, int top)
        {
            var temp_product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status && x.CategoryID == temp_product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public void IncreaseView(int id)
        {
            var product=_productRepository.GetSingleById(id);
            if(product.ViewCount.HasValue)
            {
                product.ViewCount++;
            }
            else
            {
                product.ViewCount = 1;
            }
        }

        public void SaveChanges()
        {
            _unitofWork.Commit();
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, out int totalRow, string sortType)
        {

            var query = _productRepository.GetMulti(x => x.Status && x.Name == keyword);
            switch (sortType)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] Tags = product.Tags.Split(',');
                _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                for (var i = 0; i < Tags.Length; ++i)
                {
                    var tagId = StringHelper.ToUnsignString(Tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Type = CommonConstants.ProductTag;
                        tag.Name = Tags[i];
                        _tagRepository.Add(tag);
                    }                  
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
                
            }
            _unitofWork.Commit();

        }
    }
}
