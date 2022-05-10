using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Api.Common;

namespace Insurance.Api.Data
{
    public interface IProductTypeService
    {
        Task<IList<ProductType>> Get();

        Task<ProductType> Get(int productTypeId);

        Task<bool> Post(int productTypeId, decimal surcharge);
    }
}
