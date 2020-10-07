using AlexApi.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApi.Repositories
{
    public class RequestContext : IRequestContext
    {
        private readonly IConfiguration _configuration;

        public RequestContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(string clientId)
        {
            return _configuration.GetConnectionString(clientId);
        }
    }
}
