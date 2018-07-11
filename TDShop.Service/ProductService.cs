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
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow);

        Product GetById(int id);

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }
    public class ProductService : IProductService
    {
        IProductRepository _productReponsitory;
        IUnitofWork _unitofWork;
        public ProductService(IProductRepository productRepository, IUnitofWork unitofWork)
        {
            this._productReponsitory = productRepository;
            this._unitofWork = unitofWork;
        }

        public Product Add(Product product)
        {
           return _productReponsitory.Add(product);
        }

        public Product Delete(int id)
        {
            return _productReponsitory.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productReponsitory.GetAll(new string[] { "ProductCategory" });
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _productReponsitory.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public IEnumerable<Product> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _productReponsitory.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Product GetById(int id)
        {
            return _productReponsitory.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitofWork.Commit();
        }

        public void Update(Product product)
        {
            _productReponsitory.Update(product);
        }
    }
}
