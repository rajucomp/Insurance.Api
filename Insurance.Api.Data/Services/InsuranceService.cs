using System;
using System.Collections.Generic;
using Insurance.Api.Common;
using System.Linq;

namespace Insurance.Api.Data
{
    public class InsuranceService : IInsuranceService
    {
        private decimal CalculateInsurance(decimal salesPrice, int productTypeId)
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


        public decimal CalculateInsurance(Product product, ProductType productType)
        {
            //First calcuate the normal insurance rate and then add the surcharge.
            return productType.CanBeInsured ? CalculateInsurance(product.SalesPrice, product.ProductTypeId) + productType.SurchargeRate : 0;    
        }

        //Task 3
        public decimal CalculateInsurance(OrderDto orderDto)
        {
            decimal insuranceAmount = 0.0M;

            for(int i = 0; i < orderDto.orders.Count; i++)
            {
                insuranceAmount += CalculateInsurance(orderDto.orders[i].Product, orderDto.orders[i].ProductType);
                insuranceAmount += orderDto.orders[i].ProductType.SurchargeRate;
            }

            //Task 4
            insuranceAmount += orderDto.orders.Any(x => x.ProductType.Equals(ProductTypeEnum.InsuredDigitalCameras)) ? 500 : 0;

            return insuranceAmount;
        }
    }
}
