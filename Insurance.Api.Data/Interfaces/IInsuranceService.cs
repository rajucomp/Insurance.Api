using System;
using Insurance.Api.Common;

namespace Insurance.Api.Data
{
    public interface IInsuranceService
    {
        decimal CalculateInsuranceWithSurcharge(Product product, ProductType productType);

        decimal CalculateInsuranceWithoutSurcharge(Product product, ProductType productType);

        decimal CalculateInsuranceWithoutSurcharge(OrderDto orderDto);
    }

}
