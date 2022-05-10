using System;
using System.Collections.Generic;
using Insurance.Api.Common;
using Insurance.Api.Data;
using Insurance.Api.Tests.Interfaces;
using Xunit;

namespace Insurance.Api.Tests
{
    public class InsuranceServiceTests : IInsuranceServiceTests
    {
        public InsuranceServiceTests()
        {
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000EurosAndIsACamera_ShouldAdd1500EurosToInsuranceCost()
        {
            var product = new Product()
            {

                ProductId = 836194,
                ProductName = "Sony CyberShot DSC-RX100 VII",
                SalesPrice = 1129,
                ProductTypeId = 33
            };

            var productType = new ProductType()
            {

                ProductTypeId = 33,
                ProductTypeName = "Digital cameras",
                CanBeInsured = true
            };

            const decimal expectedInsuranceValue = 1500;

            var result = new InsuranceService().CalculateInsuranceWithSurcharge(product, productType);
          
            Assert.Equal(expected: expectedInsuranceValue, actual: result, 2);
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceLessThan500EurosAndIsALaptop_ShouldAdd500EurosToInsuranceCost()
        {
            var product = new Product()
            {

                ProductId = 837856,
                ProductName = "Lenovo Chromebook C330-11 81HY000MMH",
                SalesPrice = 299,
                ProductTypeId = 21
            };

            var productType = new ProductType()
            {

                ProductTypeId = 21,
                ProductTypeName = "Laptops",
                CanBeInsured = true
            };

            const decimal expectedInsuranceValue = 500;

            var result = new InsuranceService().CalculateInsuranceWithSurcharge(product, productType);
            Assert.Equal(expected: expectedInsuranceValue, actual: result, 2);
        }

        [Fact]
        public void CalculateInsuranceForOrders()
        {
            var orderDto = new OrderDto()
            {
                OrderId = 1,
                Orders = new List<Order>()
                {
                    new Order()
                    {
                        InsuranceDto = new InsuranceDto()
                        {

                            ProductId = 837856,
                            ProductTypeName = "Lenovo Chromebook C330-11 81HY000MMH",
                            SalesPrice = 299,
                            ProductTypeId = 21,
                            SurchargeRate = true
                        },
                        Quantity = 10
                    },
                    new Order()
                    {
                        InsuranceDto = new InsuranceDto()
                        {
                            ProductId = 836194,
                            ProductTypeName = "Sony CyberShot DSC-RX100 VII",
                            SalesPrice = 1129,
                            ProductTypeId = 33,
                            SurchargeRate = true
                        },
                        Quantity = 10
                    }
                }
            };

            const decimal expectedInsuranceValue = 20000;
            var result = new InsuranceService().CalculateInsuranceWithoutSurcharge(orderDto);
            Assert.Equal(expected: expectedInsuranceValue, actual: result, 2);
        }
    }
}
