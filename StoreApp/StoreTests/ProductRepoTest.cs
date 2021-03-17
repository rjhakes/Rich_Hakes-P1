using Xunit;
using Microsoft.EntityFrameworkCore;
using StoreDL;
using Model = StoreModels;
using System.Linq;
using StoreModels;
//When unit testing DBs, note that you need to install the Microsoft.EntityFrameworkCore.Sqlite package
//Sqlite has features that allows you to create an inmemory rdb. 
namespace StoreTests
{
    /// <summary>
    /// test class for the data access methods in my DL
    /// </summary>
    public class ProductRepoTest
    {
        private readonly DbContextOptions<StoreDBContext> options;
        //Because xunit creates new instances of test classes, you need to make sure your db is seeded
        public ProductRepoTest()
        {
            //use sqlite to create an inmemory test.db
            options = new DbContextOptionsBuilder<StoreDBContext>()
            .UseSqlite("Filename=Test.db")
            .Options;
            Seed();
        }
        //testing read operations
        //When testing methods that do not change the state of the data in the db, only one context is needed
        [Fact]
        public void GetAllProductsShouldReturnAllProducts()
        {
            //This is a using block, at the end of the execution of the code surrounded by the block, the 
            //unmanaged resource is going to be disposed of 
            using (var context = new StoreDBContext(options))
            {
                //Arrange
                IProductRepository _repo = new ProductRepoDB(context);

                //Act
                var products = _repo.GetProducts();
                Assert.Equal(2, products.Count);
            }
        }
        [Fact]
        public void GetProductByEmailShouldRetuenProduct()
        {
            using (var context = new StoreDBContext(options))
            {
                IProductRepository _repo = new ProductRepoDB(context);
                var foundProduct = _repo.GetProductByName("aProduct");

                Assert.NotNull(foundProduct);
                Assert.Equal("aProduct", foundProduct.ProdName);
            }
        }
        //When testing operations that change the state of the db (i.e manipulate the data inside the db) 
        //make sure to check if the change has persisted even when accessing the db using a different context/connection
        //This means that you create another instance of your context when testing to check that the method has 
        //definitely affected the db.
        [Fact]
        public void AddProductShouldAddProduct()
        {
            using (var context = new StoreDBContext(options))
            {
                IProductRepository _repo = new ProductRepoDB(context);
                _repo.AddProduct
                (
                    new Model.Product
                    {                    
                        Id = 2,
                        ProdName = "aProduct",
                        ProdPrice = 420,
                        ProdCategory = (Category)2,
                        ProdBrandName = "aBrand",
                        Description = "Pending",

                    }
                );
            }
            //use the context to check the state of the db directly when asserting.
            using (var assertContext = new StoreDBContext(options))
            {
                var result = assertContext.Products.FirstOrDefault(product => product.ProdName == "aProduct");
                Assert.NotNull(result);
                Assert.Equal("aProduct", result.ProdName);
            }
        }
        [Fact]
        public void DeleteShouldDelete()
        {
            using (var context = new StoreDBContext(options))
            {
                IProductRepository _repo = new ProductRepoDB(context);
                _repo.DeleteProduct(
                    new Model.Product
                    {
                        Id = 1,
                        ProdName = "bProduct",
                        ProdPrice = 24,
                        ProdCategory = (Category)4,
                        ProdBrandName = "bBrand",
                        Description = "Pending",
                    }
                );
            }
            using (var assertContext = new StoreDBContext(options))
            {
                var result = assertContext.Products.Find(1);
                Assert.Null(result);
            }
        }
        [Fact]
        public void UpdateProductShouldUpdate()
        {
            using (var context = new StoreDBContext(options))
            {
                IProductRepository _repo = new ProductRepoDB(context);
                _repo.UpdateProduct(
                    new Model.Product
                    {
                        Id = 1,
                        ProdName = "cProduct",
                        ProdPrice = 520,
                        ProdCategory = (Category)2,
                        ProdBrandName = "cBrand",
                        Description = "Pending a Desc",
                    }
                );
            }
        }
        private void Seed()
        {
            using (var context = new StoreDBContext(options))
            {
                //This makes sure that the state of the db gets recreated every time to maintain the modularity of the tests.
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                context.Products.AddRange
                (
                    new Product
                    {
                        Id = 1,
                        ProdName = "bProduct",
                        ProdPrice = 24,
                        ProdCategory = (Category)4,
                        ProdBrandName = "bBrand",
                        Description = "Pending",
                    },
                    new Product
                    {
                        Id = 2,
                        ProdName = "aProduct",
                        ProdPrice = 420,
                        ProdCategory = (Category)2,
                        ProdBrandName = "aBrand",
                        Description = "Pending",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}