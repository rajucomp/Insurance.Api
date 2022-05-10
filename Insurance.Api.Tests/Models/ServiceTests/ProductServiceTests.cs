using System;
using Insurance.Api.Common;
using Insurance.Api.Data;
using Insurance.Api.Tests.Interfaces;
using Xunit;

namespace Insurance.Api.Tests
{
    public class ProductServiceTests : IProductServiceTests
    {
        [Fact]
        public void ShouldWorkForSingleProductId()
        {
            var product = new Product()
            {

                ProductId = 837856,
                ProductName = "Lenovo Chromebook C330-11 81HY000MMH",
                SalesPrice = 299,
                ProductTypeId = 21
            };

            var result = new ProductService().Get(product.ProductId).Result;
            Assert.Equal(expected: product.ProductId, actual: result.ProductId);
            Assert.Equal(expected: product.ProductName, actual: result.ProductName);
            Assert.Equal(expected: product.SalesPrice, actual: result.SalesPrice);
            Assert.Equal(expected: product.ProductTypeId, actual: result.ProductTypeId);
        }
    }
}
