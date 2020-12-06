using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dapperCRUD.DatabaseConnection;
using dapperCRUD.Models;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace dapperCRUD.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly IDatabaseConnectionFactory _dbConnection;
        public ProductQuery(IDatabaseConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ProductModel>> FetchProduct()
        {
            using var conn = await _dbConnection.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var result = db.Query("Product");
            return await result.GetAsync<ProductModel>();
        }

        public async Task<ProductModel> GetProductById(Guid id)
        {
            using var conn = await _dbConnection.CreateConnectionAsync();
            var db = new QueryFactory(conn, new SqlServerCompiler());
            var result = db.Query("Product").Where("ProductID", "=", id);
            return await result.FirstOrDefaultAsync<ProductModel>();
        }
    }
}