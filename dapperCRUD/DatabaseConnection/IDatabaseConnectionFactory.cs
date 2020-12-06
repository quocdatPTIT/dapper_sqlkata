using System.Data;
using System.Threading.Tasks;

namespace dapperCRUD.DatabaseConnection
{
    public interface IDatabaseConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}