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
    public class ProductControllerTest
    {
        [Fact]
        public void ProductControllerShouldReturnIndex()
        {
            //Arrange
            //Creating a stub of IProductBL  using Moq the framework and in Moq, fake objects
            // are called Mock
            var mockRepo = new Mock<IProductBL>();
            // This is just us defining what the stub would do/return if the method GetProductes() is called
            // We're returning a static list of customeres
            mockRepo.Setup(x => x.GetProducts())
                .Returns(new List<Product>()
                {
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
                }
               );
            //I really don't need to create a fake mapper because the real one doesn't affect the 
            // state of my data, just its type, (it just casts stuff)
            var controller = new ProductController(mockRepo.Object, new Mapper());

            //Act
            var result = controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductIndexVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());

        }
    }
}
