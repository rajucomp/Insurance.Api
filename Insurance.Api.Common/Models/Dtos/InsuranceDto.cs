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

        [JsonIgnore]
        [JsonProperty("productTypeName")]
        public string ProductTypeName { get; set; }

        [JsonIgnore]
        [JsonProperty("productTypeHasInsurance")]
        public bool ProductTypeHasInsurance { get; set; }

        [JsonIgnore]
        [JsonProperty("salesPrice")]
        public decimal SalesPrice { get; set; }
    }
}
