using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Insurance.Api.Common;
using Newtonsoft.Json;

namespace Insurance.Api.Data
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly HttpClient _httpClient;
        private const string _baseAddress = "http://localhost:5002";
        private readonly string _suffix = "/product_types";
        private readonly string _surchargeSuffix = "/product_types/surcharge";

        public ProductTypeService()
        {
            this._httpClient = new HttpClient { BaseAddress = new Uri(_baseAddress) };
        }

        public async Task<IList<ProductType>> Get()
        {
            var response = await _httpClient.GetAsync(_suffix);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ProductType>>(jsonString);
            }

            return null;
        }

        public async Task<ProductType> Get(int productTypeId)
        {
            string url = String.Format("{0}/{1}", _suffix, productTypeId);
            var response = await _httpClient.GetAsync(url);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductType>(jsonString);
            }

            return null;
        }

        public async Task<bool> Post(int productTypeId, decimal surcharge)
        {
            string url = String.Format("{0}/{1}", _surchargeSuffix, productTypeId);
            var response = await _httpClient.PostAsync(url, null);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(jsonString);
            }

            return false;
        }
    }
}
