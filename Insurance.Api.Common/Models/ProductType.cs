using System;
using Newtonsoft.Json;

namespace Insurance.Api.Common
{
    public class ProductType
    {
        [JsonProperty("id")]
        public int ProductTypeId { get; set; }

        [JsonProperty("name")]
        public string ProductTypeName { get; set; }

        [JsonProperty("canBeInsured")]
        public bool CanBeInsured { get; set; }

        [JsonProperty("surchargeRate")]
        public decimal SurchargeRate { get; set; }
    }
}
