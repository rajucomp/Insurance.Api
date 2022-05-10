using System;
using Insurance.Api.Common;
using Insurance.Api.Data;
using Insurance.Api.Tests.Interfaces;
using Moq;
using Xunit;

namespace Insurance.Api.Tests
{
    public class ProductTypeServiceTests : IProductTypeServiceTests
    {
        private readonly Mock<IProductTypeService> _mockProductTypeService;

        public ProductTypeServiceTests()
        {
            _mockProductTypeService = new Mock<IProductTypeService>();
        }

        [Fact]
        public void ShouldWorkForSingleProductTypeId()
        {
            //Arrange
            var productType = new ProductType()
            {

                ProductTypeId = 21,
                ProductTypeName = "Laptops",
                CanBeInsured = true
            };
            _mockProductTypeService.Setup(p => p.Get(productType.ProductTypeId)).ReturnsAsync(productType);

            //Act
            var result = _mockProductTypeService.Object.Get(productType.ProductTypeId).Result;

            //Assert
            Assert.Equal(expected: productType, actual: result);
            _mockProductTypeService.Verify(p => p.Get(productType.ProductTypeId), Times.Once);
        }

        [Fact]
        public void ShouldReturnFalseWhilePostingSurchargeForInValidProductId()
        {
            // Arrange
            _mockProductTypeService.Setup(p => p.Post(-2, 1));
            _mockProductTypeService.Setup(p => p.Post(0, 1));
            _mockProductTypeService.Setup(p => p.Post(456743537, 0));

            //Act
            var result = _mockProductTypeService.Object.Post(-2, 1);
            var result1 = _mockProductTypeService.Object.Post(-2, 1);
            var result2 = _mockProductTypeService.Object.Post(456743537, 0);

            //Assert
            Assert.False(result.Result);
            Assert.False(result1.Result);
            Assert.False(result2.Result);
            _mockProductTypeService.Verify(p => p.Post(-2, 1), Times.AtLeastOnce);
        }

        [Fact]
        public void ShouldReturnNullForInvalidProductId()
        {
            //Arrange
            _mockProductTypeService.Setup(p => p.Get(-2));
            _mockProductTypeService.Setup(p => p.Get(456743537));

            //Act
            var result = _mockProductTypeService.Object.Get(456743537);
            var result1 = _mockProductTypeService.Object.Get(-2);

            //Assert
            Assert.Null(result.Result);
            Assert.Null(result1.Result);
            _mockProductTypeService.Verify(p => p.Get(456743537), Times.AtLeastOnce);
        }
    }
}
