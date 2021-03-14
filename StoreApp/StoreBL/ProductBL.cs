using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class ProductBL : IProductBL
    {
        private IProductRepository _repo;

        public ProductBL(IProductRepository repo) {
            _repo = repo;
        }

        public Product AddProduct(Product newProduct)
        {
            //TODO: Add BL
            return _repo.AddProduct(newProduct);
        }
        public Product DeleteProduct(Product product2BDeleted)
        {
            return _repo.DeleteProduct(product2BDeleted);
        }
        public Product GetProductByName(string name) {
            //todo validate
            return _repo.GetProductByName(name);
        }
        public Product GetProductById(int id) {
            //todo validate
            return _repo.GetProductById(id);
        }
        public List<Product> GetProducts()
        {
            //TODO Add BL
            return _repo.GetProducts();
        }
        public Product UpdateProduct(Product product2BUpdated)
        {
            return _repo.UpdateProduct(product2BUpdated);
        }
    }
}
