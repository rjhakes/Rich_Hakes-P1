using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using StoreBL;
using StoreModels;
using StoreMVC.Controllers;
using StoreMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace StoreTests
{
    public class ManagerControllerTest
    {
        [Fact]
        public void ManagerControllerShouldReturnIndex()
        {
            //Arrange
            //Creating a stub of IManagerBL  using Moq the framework and in Moq, fake objects
            // are called Mock
            var mockRepo = new Mock<IManagerBL>();
            // This is just us defining what the stub would do/return if the method GetManageres() is called
            // We're returning a static list of customeres
            mockRepo.Setup(x => x.GetManagers())
                .Returns(new List<Manager>()
                {
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
                }
               );
            //I really don't need to create a fake mapper because the real one doesn't affect the 
            // state of my data, just its type, (it just casts stuff)
            var controller = new ManagerController(mockRepo.Object, new Mapper());

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ManagerIndexVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());

        }
    }
}
