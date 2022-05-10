using System;
using Insurance.Api.Common;
using Insurance.Api.Data;
using Insurance.Api.Tests.Interfaces;
using Xunit;

namespace Insurance.Api.Tests
{
    public class ProductTypeServiceTests : IProductTypeServiceTests
    {
        [Fact]
        public void ShouldWorkForSingleProductTypeId()
        {
            var productType = new ProductType()
            {

                ProductTypeId = 21,
                ProductTypeName = "Laptops",
                CanBeInsured = true
            };

            var result = new ProductTypeService().Get(productType.ProductTypeId).Result;
            Assert.Equal(expected: productType.ProductTypeId, actual: result.ProductTypeId);
            Assert.Equal(expected: productType.ProductTypeName, actual: result.ProductTypeName);
            Assert.Equal(expected: productType.CanBeInsured, actual: result.CanBeInsured);
        }
    }
}
