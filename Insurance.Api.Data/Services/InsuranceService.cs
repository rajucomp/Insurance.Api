using System;
using System.Collections.Generic;
using Insurance.Api.Common;
using System.Linq;

namespace Insurance.Api.Data
{
    public class InsuranceService : IInsuranceService
    {
        decimal CalculateInsurance(decimal salesPrice, int productTypeId)
        {
            decimal insuranceAmount = 0.0M;

            if (salesPrice < 500)
            {
                insuranceAmount = 0;
            }
            else
            {
                if (salesPrice >= 500 && salesPrice < 2000)
                {
                    insuranceAmount = 1000;
                }
                else if (salesPrice >= 2000)
                {
                    insuranceAmount = 2000;
                }
            }

            // It is better to to use productTypeId for comparison rather than
            // productTypeName as names can change in the future but not the ids.

            /*
            if (productTypeName == "Laptops" || productTypeName == "Smartphones")
            {
                insuranceAmount += 500; ;
            }
            */

            if (productTypeId == (int)ProductTypeEnum.InsuredLaptops || productTypeId == (int)ProductTypeEnum.Smartphones)
            {
                insuranceAmount += 500; ;
            }

            return insuranceAmount;
        }


        public decimal CalculateInsuranceWithSurcharge(Product product, ProductType productType)
        {
            //First calculate the normal insurance rate and then add the surcharge.
            return productType.CanBeInsured ? CalculateInsurance(product.SalesPrice, product.ProductTypeId) + productType.SurchargeRate : 0;    
        }

        public decimal CalculateInsuranceWithoutSurcharge(Product product, ProductType productType)
        {
            //First calculate the normal insurance rate and then add the surcharge.
            return productType.CanBeInsured ? CalculateInsurance(product.SalesPrice, product.ProductTypeId) : 0;
        }

        public decimal CalculateInsuranceWithoutSurcharge(OrderDto orderDto)
        {
            decimal insuranceAmount = 0;

            foreach(var product in orderDto.Orders)
            {
                insuranceAmount += CalculateInsurance(product.InsuranceDto.SalesPrice, product.InsuranceDto.ProductTypeId) * product.Quantity;
            }

            return insuranceAmount;
        }
    }
}
