using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AlexApi.Repositories.Abstract
{
    public class ConnectionProvider
    {
        private readonly IConfiguration configuration;
        public ConnectionProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected IDbConnection CreateConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
