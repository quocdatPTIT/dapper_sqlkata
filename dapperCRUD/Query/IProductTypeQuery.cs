using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dapperCRUD.Models;

namespace dapperCRUD.Query
{
    public interface IProductTypeQuery
    {
        Task<List<ProductTypeModel>> GetAllAsync();
        Task<List<ProductTypeResponse>> GetProductDetail();
        Task<ProductTypeModel> GetProductTypeDetail(Guid id);
        Task<int> InsertProductType(ProductTypeCreateModel input);
    }
}