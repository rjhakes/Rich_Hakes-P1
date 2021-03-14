using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public interface IProductBL
    {
        List<Product> GetProducts();
        Product AddProduct(Product newProduct);
        Product GetProductByName(string name);
        Product GetProductById(int id);
        Product DeleteProduct(Product product2BDeleted);
        Product UpdateProduct(Product product2BUpdated);
    }
}