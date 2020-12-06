using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dapperCRUD.DatabaseConnection;
using dapperCRUD.Models;
using dapperCRUD.Query;
using Microsoft.AspNetCore.Mvc;

namespace dapperCRUD.Controllers
{
    [ApiController]
    [Route("api/product-type")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeQuery _productTypeQuery;
        
        public ProductTypeController(IProductTypeQuery productTypeQuery)
        {
            _productTypeQuery = productTypeQuery;
        }

        [HttpGet]
        [Route("product-type")]
        public async Task<IEnumerable<ProductTypeModel>> Get()
        {
            var productType = await _productTypeQuery.GetAllAsync();
            return await Task.FromResult(productType);
        }

        [HttpGet]
        [Route("product-detail")]
        public async Task<List<ProductTypeResponse>> GetProductDetail()
        {
            var product = await _productTypeQuery.GetProductDetail();
            return await Task.FromResult(product);
        }

        [HttpGet]
        [Route("product-type/{id}")]
        public async Task<ProductTypeModel> GetProductTypeDetail(Guid id)
        {
            var productType = await _productTypeQuery.GetProductTypeDetail(id);
            return await Task.FromResult(productType);
        }

        [HttpPost]
        [Route("create-product-type")]
        public async Task<int> InsertProductType(ProductTypeCreateModel input)
        {
            var id = await _productTypeQuery.InsertProductType(input);
            return await Task.FromResult(id);
        }
    }
}