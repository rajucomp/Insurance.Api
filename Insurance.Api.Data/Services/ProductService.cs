using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Insurance.Api.Common;
using Newtonsoft.Json;

namespace Insurance.Api.Data
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private const string _baseAddress = "http://localhost:5002";
        private readonly string _suffix = "/products";

        public ProductService()
        {
            this._httpClient = new HttpClient { BaseAddress = new Uri(_baseAddress) };
        }

        public async Task<IList<Product>> Get()
        {
            var response = await _httpClient.GetAsync(_suffix);

            if (response != null && response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Product>>(jsonString);
            }

            return null;
        }

        public async Task<Product> Get(int productId)
        {
            string url = String.Format("{0}/{1}", _suffix, productId);
            var response = await _httpClient.GetAsync(url);

            if (response != null && response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(jsonString);
            }

            return null;
        }
    }
}
