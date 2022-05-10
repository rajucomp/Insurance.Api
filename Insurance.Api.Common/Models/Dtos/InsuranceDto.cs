using System;
using Newtonsoft.Json;

namespace Insurance.Api.Common
{
    public class InsuranceDto
    {
        [JsonProperty("productId")]
        public int ProductId { get; set; }

        //We should use decimal rather than float.
        [JsonProperty("insuranceValue")]
        public decimal InsuranceValue { get; set; }

        [JsonProperty("productTypeName")]
        public string ProductTypeName { get; set; }

        [JsonProperty("productTypeHasInsurance")]
        public bool ProductTypeHasInsurance { get; set; }

        [JsonProperty("salesPrice")]
        public decimal SalesPrice { get; set; }

        [JsonProperty("productTypeId")]
        public int ProductTypeId { get; set; }

        [JsonProperty("surchargeRate")]
        public bool SurchargeRate { get; set; }
    }
}
