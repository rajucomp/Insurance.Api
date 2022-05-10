using System;
namespace Insurance.Api.Tests.Interfaces
{
    public interface IInsuranceServiceTests
    {
        void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost();

        void CalculateInsurance_GivenSalesPriceLessThan500EurosAndIsALaptop_ShouldAdd500EurosToInsuranceCost();

        void CalculateInsuranceForOrders();
    }
}
