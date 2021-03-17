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
    public class CustomerControllerTest
    {
        [Fact]
        public void CustomerControllerShouldReturnIndex()
        {
            //Arrange
            //Creating a stub of ICustomerBL  using Moq the framework and in Moq, fake objects
            // are called Mock
            var mockRepo = new Mock<ICustomerBL>();
            var mockRepo2 = new Mock<ICustomerCartBL>();
            var mockRepo3 = new Mock<ICustomerOrderLineItemBL>();
            var mockRepo5 = new Mock<ILocationBL>();
            var mockRepo6 = new Mock<IProductBL>();
            var mockRepo7 = new Mock<ICustomerOrderHistoryBL>();
            var mockRepo8  = new Mock<IInventoryLineItemBL>();
            // This is just us defining what the stub would do/return if the method GetCustomeres() is called
            // We're returning a static list of customeres
            mockRepo.Setup(x => x.GetCustomers())
                .Returns(new List<Customer>()
                {
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
                }
               );
            //I really don't need to create a fake mapper because the real one doesn't affect the 
            // state of my data, just its type, (it just casts stuff)
            var controller = new CustomerController(mockRepo.Object, mockRepo2.Object, mockRepo3.Object, mockRepo5.Object, mockRepo6.Object, mockRepo7.Object, mockRepo8.Object, new Mapper());

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerIndexVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());

        }
    }
}
