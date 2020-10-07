using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Interfaces
{
    public class ConnectionProvider : IConnectionProvider
    {
        private readonly IRequestContext _context;

        public ConnectionProvider(IRequestContext context)
        {
            _context = context;
        }

        public async Task<IDbConnection> CreateConnection(string connectionName)
        {
            var connection = new SqlConnection(_context.GetConnectionString(connectionName));
            await connection.OpenAsync();
            return connection;
        }
    }
}
