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
    public class ManagerRepoTest
    {
        private readonly DbContextOptions<StoreDBContext> options;
        //Because xunit creates new instances of test classes, you need to make sure your db is seeded
        public ManagerRepoTest()
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
        public void GetAllManagersShouldReturnAllManagers()
        {
            //This is a using block, at the end of the execution of the code surrounded by the block, the 
            //unmanaged resource is going to be disposed of 
            using (var context = new StoreDBContext(options))
            {
                //Arrange
                IManagerRepository _repo = new ManagerRepoDB(context);

                //Act
                var managers = _repo.GetManagers();
                Assert.Equal(2, managers.Count);
            }
        }
        [Fact]
        public void GetManagerByEmailShouldRetuenManager()
        {
            using (var context = new StoreDBContext(options))
            {
                IManagerRepository _repo = new ManagerRepoDB(context);
                var foundManager = _repo.GetManagerByEmail("mermaidensman@fish.fin");

                Assert.NotNull(foundManager);
                Assert.Equal("mermaidensman@fish.fin", foundManager.ManagerEmail);
            }
        }
        //When testing operations that change the state of the db (i.e manipulate the data inside the db) 
        //make sure to check if the change has persisted even when accessing the db using a different context/connection
        //This means that you create another instance of your context when testing to check that the method has 
        //definitely affected the db.
        [Fact]
        public void AddManagerShouldAddManager()
        {
            using (var context = new StoreDBContext(options))
            {
                IManagerRepository _repo = new ManagerRepoDB(context);
                _repo.AddManager
                (
                    new Model.Manager
                    {                    
                        Id = 2,
                        ManagerName = "Batman",
                        ManagerEmail = "batmail@jl.org",
                        ManagerPasswordHash = "Pa55WordI23",
                        ManagerPhone = "7948062079",
                        ManagerLocId = 1                
                    }
                );
            }
            //use the context to check the state of the db directly when asserting.
            using (var assertContext = new StoreDBContext(options))
            {
                var result = assertContext.Managers.FirstOrDefault(manager => manager.ManagerName == "Batman");
                Assert.NotNull(result);
                Assert.Equal("Batman", result.ManagerName);
            }
        }
        [Fact]
        public void DeleteShouldDelete()
        {
            using (var context = new StoreDBContext(options))
            {
                IManagerRepository _repo = new ManagerRepoDB(context);
                _repo.DeleteManager(
                    new Model.Manager
                    {
                        Id = 1,
                        ManagerName = "Aquaman",
                        ManagerEmail = "mermaidensman@fish.fin",
                        ManagerPasswordHash = "PassW0rd12E",
                        ManagerPhone = "9702608497",
                        ManagerLocId = 2
                    }
                );
            }
            using (var assertContext = new StoreDBContext(options))
            {
                var result = assertContext.Managers.Find(1);
                Assert.Null(result);
            }
        }
        [Fact]
        public void UpdateManagerShouldUpdate()
        {
            using (var context = new StoreDBContext(options))
            {
                IManagerRepository _repo = new ManagerRepoDB(context);
                _repo.UpdateManager(
                    new Model.Manager
                    {
                        Id = 1,
                        ManagerName = "Aquaperson",
                        ManagerEmail = "mermenssman@fish.fin",
                        ManagerPasswordHash = "PassW0rd12E",
                        ManagerPhone = "9702608497",
                        ManagerLocId = 1
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


                context.Managers.AddRange
                (
                    new Manager
                    {
                        Id = 1,
                        ManagerName = "Aquaman",
                        ManagerEmail = "mermaidensman@fish.fin",
                        ManagerPasswordHash = "PassW0rd12E",
                        ManagerPhone = "9702608497",
                        ManagerLocId = 1
                    },
                    new Manager
                    {
                        Id = 2,
                        ManagerName = "Batman",
                        ManagerEmail = "batmail@jl.org",
                        ManagerPasswordHash = "Pa55WordI23",
                        ManagerPhone = "7948062079",
                        ManagerLocId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}