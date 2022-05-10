using System;
namespace Insurance.Api.Tests.Interfaces
{
    public interface IInsuranceServiceTests
    {
        void CalculateInsurance_GivenSalesPriceBetween500And2000EurosAndIsACamera_ShouldAdd1500EurosToInsuranceCost();

        void CalculateInsurance_GivenSalesPriceLessThan500EurosAndIsALaptop_ShouldAdd500EurosToInsuranceCost();

        void CalculateInsuranceForOrders();
    }
}
