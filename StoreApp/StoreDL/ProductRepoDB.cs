using StoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreDL
{
    public class ProductRepoDB : IProductRepository
    {
        private readonly StoreDBContext _context;
        public ProductRepoDB(StoreDBContext context)
        {
            _context = context;
        }
        public Product AddProduct(Product newProduct)
        {
            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return newProduct;
        }

        public Product DeleteProduct(Product product2BDeleted)
        {
            _context.Products.Remove(product2BDeleted);
            _context.SaveChanges();
            return product2BDeleted;
        }

        public Product GetProductByName(string name)
        {
            return _context.Products
                .FirstOrDefault(x => x.ProdName == name);
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetProducts()
        {
            return _context.Products
                .Select(x => x)
                .ToList();
        }

        public Product UpdateProduct(Product product2BUpdated)
        {
            Product oldProduct = _context.Products.Find(product2BUpdated.Id);


            _context.Entry(oldProduct).CurrentValues.SetValues(product2BUpdated);

            _context.SaveChanges();

            //This method clears the change tracker to drop all tracked entities
            _context.ChangeTracker.Clear();
            return product2BUpdated;

        }
    }
}
