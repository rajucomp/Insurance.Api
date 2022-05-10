using System;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using Insurance.Api.Common;
using Insurance.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    public class InsuranceController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        private readonly IInsuranceService _insuranceService;
        private readonly ILogger<InsuranceController> _logger;

        public InsuranceController(IProductService productService, IProductTypeService productTypeService, IInsuranceService insuranceService, ILogger<InsuranceController> logger)
        {
            _productService = productService;
            _productTypeService = productTypeService;
            _insuranceService = insuranceService;
            _logger = logger;
        }

        [HttpPost, Route("/product")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InsuranceDto> CalculateInsurance([FromBody] InsuranceDto insuranceDto)
        {
            try
            {
                if(insuranceDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                if (!productExists(insuranceDto.ProductId))
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                insuranceDto.InsuranceValue = CalculateInsurance(insuranceDto.ProductId);

                return Ok(insuranceDto);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost, Route("/order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<OrderDto> CalculateInsuranceForOrder([FromBody] OrderDto orderDto)
        {
            try
            {
                if (orderDto == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                orderDto.InsuranceAmount = _insuranceService.CalculateInsurance(orderDto);

                return Ok(orderDto);
            }
            catch(Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message.ToString());

                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "error occurred" });
            }
            
        }

        bool productExists(int productId)
        {
            return _productService.Get(productId).Result != null;
        }

        decimal CalculateInsurance(int productId)
        {
            var product = _productService.Get(productId).Result;

            var productType = _productTypeService.Get(product.ProductTypeId).Result;

            return _insuranceService.CalculateInsurance(product, productType);
        }
    }
}