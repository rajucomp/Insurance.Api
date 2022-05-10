using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Api.Common;

namespace Insurance.Api.Data
{
    public interface IProductService
    {
        Task<IList<Product>> Get();

        Task<Product> Get(int productId);
    }
}
