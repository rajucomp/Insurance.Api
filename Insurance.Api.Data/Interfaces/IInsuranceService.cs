using System;
using Insurance.Api.Common;

namespace Insurance.Api.Data
{
    public interface IInsuranceService
    {
        decimal CalculateInsurance(Product product, ProductType productType);

        decimal CalculateInsurance(OrderDto orderDto);
    }
}
