using System;
using Insurance.Api.Common;
using Insurance.Api.Data;
using Insurance.Api.Tests.Interfaces;
using Moq;
using Xunit;

namespace Insurance.Api.Tests
{
    public class ProductServiceTests : IProductServiceTests
    {
        private readonly Mock<IProductService> _mockProductService;
        public ProductServiceTests()
        {
            _mockProductService = new Mock<IProductService>();
        }

        [Fact]
        public void ShouldWorkForSingleProductId()
        {
            //Arrange
            var product = new Product()
            {

                ProductId = 837856,
                ProductName = "Lenovo Chromebook C330-11 81HY000MMH",
                SalesPrice = 299,
                ProductTypeId = 21
            };
            _mockProductService.Setup(p => p.Get(product.ProductId)).ReturnsAsync(product);

            //Act
            var result = _mockProductService.Object.Get(product.ProductId);

            //Assert
            Assert.Equal(product, result.Result);
            _mockProductService.Verify(p => p.Get(product.ProductId), Times.Once);
        }

        [Fact]
        public void ShouldReturnNullForInvalidProductId()
        {
            //Arrange
            _mockProductService.Setup(p => p.Get(456743537));
            _mockProductService.Setup(p => p.Get(-2));

            //Act
            var result = _mockProductService.Object.Get(456743537);
            var result1 = _mockProductService.Object.Get(-2);

            //Assert
            Assert.Null(result.Result);
            Assert.Null(result1.Result);
            _mockProductService.Verify(p => p.Get(456743537), Times.Once);
            _mockProductService.Verify(p => p.Get(-2), Times.Once);
        }
    }
}
