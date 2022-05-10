using System;
using Newtonsoft.Json;

namespace Insurance.Api.Common
{
    public class Product
    {
        [JsonProperty("id")]
        public int ProductId { get; set; }

        [JsonProperty("name")]
        public string ProductName { get; set; }

        [JsonProperty("salesPrice")]
        public decimal SalesPrice { get; set; }

        [JsonProperty("productTypeId")]
        public int ProductTypeId { get; set; }
    }
}
