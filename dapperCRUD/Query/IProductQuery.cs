using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dapperCRUD.Models;

namespace dapperCRUD.Query
{
    public interface IProductQuery
    {
        Task<IEnumerable<ProductModel>> FetchProduct();

        Task<ProductModel> GetProductById(Guid id);
    }
}