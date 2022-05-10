using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Insurance.Api.Common
{
    public class OrderDto
    {
        [JsonProperty("id")]
        public int orderId { get; set; }

        [JsonProperty("orders")]
        public IList<Order> orders { get; set; }

        [JsonProperty("insuranceAmount")]
        public decimal InsuranceAmount { get; set; }
    }

    public class Order
    {
        [JsonProperty("product")]
        public Product Product { get; set; }

        [JsonProperty("productType")]
        public ProductType ProductType { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }
    }
}
