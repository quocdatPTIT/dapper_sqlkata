using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using dapperCRUD.DatabaseConnection;
using dapperCRUD.Models;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace dapperCRUD.Query
{
    public class ProductTypeQuery : IProductTypeQuery
    {
        private readonly IDatabaseConnectionFactory _dbConnection;

        public ProductTypeQuery(IDatabaseConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<ProductTypeModel>> GetAllAsync()
        {
            using var conn = await _dbConnection.CreateConnectionAsync();
            const string sql = "SELECT * FROM ProductType";
            var result = (await conn.QueryAsync<ProductTypeModel>(sql)).ToList();
            return result;
        }

        public async Task<List<ProductTypeResponse>> GetProductDetail()
        {
            using var conn = await _dbConnection.CreateConnectionAsync();
            const string sql = @"SELECT * FROM ProductType 
                        JOIN Product on ProductType.ProductTypeID = Product.ProductTypeID";
            var lookup = new Dictionary<Guid, ProductTypeResponse>();
            var unused = (await conn.QueryAsync<ProductTypeResponse, ProductModel, ProductTypeResponse>(sql,
                (productType, product) =>
                {
                    if (!lookup.TryGetValue(productType.ProductTypeID, out var productTypeModel)) {
                        lookup.Add(productType.ProductTypeID, productTypeModel = productType);
                    }

                    productTypeModel.Products ??= new List<ProductModel>();
                    productTypeModel.Products.Add(product);
                    return productTypeModel;
                },
                splitOn: "ProductID")).AsQueryable();
            return await Task.FromResult(lookup.Values.ToList());
        }

        public async Task<ProductTypeModel> GetProductTypeDetail(Guid id)
        {
            using var conn = await _dbConnection.CreateConnectionAsync();
            var parameters = new { ProductTypeID = id };
            const string sql = @"SELECT * FROM ProductType WHERE ProductTypeID = @ProductTypeID";
            var result = await conn.QueryFirstOrDefaultAsync<ProductTypeModel>(sql, parameters);
            return await Task.FromResult(result);
        }

        public async Task<int> InsertProductType(ProductTypeCreateModel input)
        {
            using var conn = await _dbConnection.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var affectedRecord = await db.Query("ProductType").InsertAsync(new ProductTypeModel
            {
                ProductTypeID = Guid.NewGuid(),
                ProductTypeKey = input.ProductTypeKey,
                ProductTypeName = input.ProductTypeName,
                RecordStatus = input.RecordStatus,
                CreatedDate = DateTime.Now,
            });

            return await Task.FromResult(affectedRecord);
        }
    }
}