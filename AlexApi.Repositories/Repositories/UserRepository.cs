using AlexApi.Domain.Models;
using AlexApi.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConnectionProvider _provider;

        public UserRepository(IConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task<int> AddAsync(User entity, string clientId)
        {
            var sql = "InsertUsers";
            
            using (var connection = _provider.CreateConnection(clientId))
            {
                return await connection.Result.ExecuteAsync(
                    sql, 
                    new { userName = entity.UserName, password = entity.Password }, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Task<User> GetAsync(User entity, string clientId)
        {
            var sql = "SelectUsers";
            using (var connection = _provider.CreateConnection(clientId))
            {

                return connection.Result.QuerySingleOrDefaultAsync<User>(
                    sql, 
                    new { userName = entity.UserName, password = entity.Password },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
