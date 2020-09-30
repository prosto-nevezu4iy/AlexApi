using AlexApi.Domain.Models;
using AlexApi.Repositories.Abstract;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Repositories
{
    public class UserRepository : ConnectionProvider, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<int> AddAsync(User entity)
        {
            var sql = "Insert into Users (UserName,Password) VALUES (@UserName,@Password)";
            
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<User> GetAsync(string userName, string password)
        {
            var sql = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password";
            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { UserName = userName, Password = password });
            }
        }
    }
}
