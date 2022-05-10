using System;
using Insurance.Api.Common;
using Insurance.Api.Controllers;
using Insurance.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Xunit;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Tests
{
    public class InsuranceControllerTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;
        private readonly Mock<IProductService> _mockProductService;
        private readonly Mock<IProductTypeService> _mockProductTypeService;
        private readonly Mock<IInsuranceService> _mockInsuranceService;
        private readonly Mock<ILogger<InsuranceController>> _mockLogger;


        public InsuranceControllerTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
            _mockProductService = new Mock<IProductService>();
            _mockProductTypeService = new Mock<IProductTypeService>();
            _mockInsuranceService = new Mock<IInsuranceService>();
            _mockLogger = new Mock<ILogger<InsuranceController>>();
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
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

            var insuranceDto = new InsuranceDto
            {
                ProductId = 836194,
                InsuranceValue = 1000
            };

            const decimal expectedInsuranceValue = 1000;

            _mockProductService.Setup(p => p.Get(product.ProductId)).ReturnsAsync(product);
            _mockProductTypeService.Setup(p => p.Get(product.ProductTypeId)).ReturnsAsync(productType);
            _mockInsuranceService.Setup(p => p.CalculateInsuranceWithoutSurcharge(product, productType)).Returns(expectedInsuranceValue);
            
            var insuranceController = new InsuranceController(_mockProductService.Object, _mockProductTypeService.Object, _mockInsuranceService.Object, _mockLogger.Object);

            var result = insuranceController.CalculateInsurance(insuranceDto);
            InsuranceDto actualInsuranceValue = (result.Result as OkObjectResult).Value as InsuranceDto;

            Assert.Equal(expected: expectedInsuranceValue, actual: actualInsuranceValue.InsuranceValue, 2);
        }

        [Fact]
        public void CalculateInsurance_GivenSalesPriceLessThan500EurosAndIsALaptop_ShouldAdd500EurosToInsuranceCost()
        {
            var product = new Product()
            {

                ProductId = 837856,
                ProductName = "Lenovo Chromebook C330-11 81HY000MMH",
                SalesPrice = 299,
                ProductTypeId = 21,
            };

            var productType = new ProductType()
            {

                ProductTypeId = 21,
                ProductTypeName = "Laptops",
                CanBeInsured = true
            };

            var insuranceDto = new InsuranceDto
            {
                ProductId = 837856,
                InsuranceValue = 500
            };

            const decimal expectedInsuranceValue = 500;

            _mockProductService.Setup(p => p.Get(product.ProductId)).ReturnsAsync(product);
            _mockProductTypeService.Setup(p => p.Get(product.ProductTypeId)).ReturnsAsync(productType); ;
            _mockInsuranceService.Setup(p => p.CalculateInsuranceWithoutSurcharge(product, productType)).Returns(expectedInsuranceValue);
            

            
            var insuranceController = new InsuranceController(_mockProductService.Object, _mockProductTypeService.Object, _mockInsuranceService.Object, _mockLogger.Object);

            var result = insuranceController.CalculateInsurance(insuranceDto);

            var actualInsuranceValue = (result.Result as OkObjectResult).Value as InsuranceDto;

            Assert.Equal(expected: expectedInsuranceValue, actual: actualInsuranceValue.InsuranceValue, 2);
        }

        [Fact]
        public void ShouldReturn400BadRequestInCaseOfInvalidRequest()
        {
            var insuranceController = new InsuranceController(_mockProductService.Object, _mockProductTypeService.Object, _mockInsuranceService.Object, _mockLogger.Object);
            var result = insuranceController.CalculateInsurance(null);
            var response = (result.Result as StatusCodeResult);
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Fact]
        public void ShouldReturn404NotFoundInCaseOfInvalidProductId()
        {
            var insuranceDto = new InsuranceDto
            {
                ProductId = 1,
                InsuranceValue = 500
            };
            var insuranceController = new InsuranceController(_mockProductService.Object, _mockProductTypeService.Object, _mockInsuranceService.Object, _mockLogger.Object);
            var result = insuranceController.CalculateInsurance(insuranceDto);
            var response = (result.Result as StatusCodeResult);
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Fact]
        public void ShouldReturn400BadRequestInCaseOfInvalidOrder()
        {
            var insuranceController = new InsuranceController(_mockProductService.Object, _mockProductTypeService.Object, _mockInsuranceService.Object, _mockLogger.Object);
            var result = insuranceController.CalculateInsuranceForOrder(null);
            var response = (result.Result as StatusCodeResult);
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }
    }

    public class ControllerTestFixture : IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            _host = new HostBuilder()
                   .ConfigureWebHostDefaults(
                        b => b.UseUrls("http://localhost:5006")
                              .UseStartup<ControllerTestStartup>()
                    )
                   .Build();

            _host.Start();
        }

        public void Dispose() => _host.Dispose();
    }

    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string)context.Request.RouteValues["id"]);
                            var product = new
                            {
                                id = productId,
                                name = "Test Product",
                                productTypeId = 1,
                                salesPrice = 750
                            };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}
