using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dapperCRUD.Models;
using dapperCRUD.Query;
using Microsoft.AspNetCore.Mvc;

namespace dapperCRUD.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductQuery _productQuery;

        public ProductController(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IEnumerable<ProductModel>> GetAllProduct()
        {
            var products = await _productQuery.FetchProduct();
            return await Task.FromResult(products);
        }

        [HttpGet]
        [Route("product-by-id/{id}")]
        public async Task<ProductModel> GetProductById(Guid id)
        {
            var product = await _productQuery.GetProductById(id);
            return await Task.FromResult(product);
        }
    }
}