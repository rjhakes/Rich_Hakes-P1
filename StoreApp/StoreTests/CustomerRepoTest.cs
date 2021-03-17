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
    public class CustomerRepoTest
    {
        private readonly DbContextOptions<StoreDBContext> options;
        //Because xunit creates new instances of test classes, you need to make sure your db is seeded
        public CustomerRepoTest()
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
        public void GetAllCustomersShouldReturnAllCustomers()
        {
            //This is a using block, at the end of the execution of the code surrounded by the block, the 
            //unmanaged resource is going to be disposed of 
            using (var context = new StoreDBContext(options))
            {
                //Arrange
                ICustomerRepository _repo = new CustomerRepoDB(context);

                //Act
                var customers = _repo.GetCustomers();
                Assert.Equal(2, customers.Count);
            }
        }
        [Fact]
        public void GetCustomerByEmailShouldRetuenCustomer()
        {
            using (var context = new StoreDBContext(options))
            {
                ICustomerRepository _repo = new CustomerRepoDB(context);
                var foundCustomer = _repo.GetCustomerByEmail("mermaidensman@fish.fin");

                Assert.NotNull(foundCustomer);
                Assert.Equal("mermaidensman@fish.fin", foundCustomer.CustomerEmail);
            }
        }
        //When testing operations that change the state of the db (i.e manipulate the data inside the db) 
        //make sure to check if the change has persisted even when accessing the db using a different context/connection
        //This means that you create another instance of your context when testing to check that the method has 
        //definitely affected the db.
        [Fact]
        public void AddCustomerShouldAddCustomer()
        {
            using (var context = new StoreDBContext(options))
            {
                ICustomerRepository _repo = new CustomerRepoDB(context);
                _repo.AddCustomer
                (
                    new Model.Customer
                    {                    
                        Id = 2,
                        CustomerName = "Batman",
                        CustomerEmail = "batmail@jl.org",
                        CustomerPasswordHash = "Pa55WordI23",
                        CustomerPhone = "7948062079",
                        CustomerAddress = "Batstreet, Batcity, Batstate Batcode"                    
                    }
                );
            }
            //use the context to check the state of the db directly when asserting.
            using (var assertContext = new StoreDBContext(options))
            {
                var result = assertContext.Customers.FirstOrDefault(customer => customer.CustomerName == "Batman");
                Assert.NotNull(result);
                Assert.Equal("Batman", result.CustomerName);
            }
        }
        [Fact]
        public void DeleteShouldDelete()
        {
            using (var context = new StoreDBContext(options))
            {
                ICustomerRepository _repo = new CustomerRepoDB(context);
                _repo.DeleteCustomer(
                    new Model.Customer
                    {
                        Id = 1,
                        CustomerName = "Aquaman",
                        CustomerEmail = "mermaidensman@fish.fin",
                        CustomerPasswordHash = "PassW0rd12E",
                        CustomerPhone = "9702608497",
                        CustomerAddress = "a street, a city, a state zipcode"
                    }
                );
            }
            using (var assertContext = new StoreDBContext(options))
            {
                var result = assertContext.Customers.Find(1);
                Assert.Null(result);
            }
        }
        [Fact]
        public void UpdateCustomerShouldUpdate()
        {
            using (var context = new StoreDBContext(options))
            {
                ICustomerRepository _repo = new CustomerRepoDB(context);
                _repo.UpdateCustomer(
                    new Model.Customer
                    {
                        Id = 1,
                        CustomerName = "Aquaperson",
                        CustomerEmail = "mermenssman@fish.fin",
                        CustomerPasswordHash = "PassW0rd12E",
                        CustomerPhone = "9702608497",
                        CustomerAddress = "a street, a city, a state zipcode"
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


                context.Customers.AddRange
                (
                    new Customer
                    {
                        Id = 1,
                        CustomerName = "Aquaman",
                        CustomerEmail = "mermaidensman@fish.fin",
                        CustomerPasswordHash = "PassW0rd12E",
                        CustomerPhone = "9702608497",
                        CustomerAddress = "a street, a city, a state zipcode"
                    },
                    new Customer
                    {
                        Id = 2,
                        CustomerName = "Batman",
                        CustomerEmail = "batmail@jl.org",
                        CustomerPasswordHash = "Pa55WordI23",
                        CustomerPhone = "7948062079",
                        CustomerAddress = "Batstreet, Batcity, Batstate Batcode"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}