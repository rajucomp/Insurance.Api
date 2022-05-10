using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Insurance.Api.Common
{
    public class OrderDto
    {
        [JsonProperty("id")]
        public int OrderId { get; set; }

        [JsonProperty("orders")]
        public IList<Order> Orders { get; set; }

        [JsonProperty("insuranceAmount")]
        public decimal InsuranceAmount { get; set; }
    }

    public class Order
    {
        [JsonProperty("insuranceDto")]
        public InsuranceDto InsuranceDto { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
