using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace AlexApi.Repositories.Interfaces
{
    public interface IRequestContext
    {
        string GetConnectionString(string clientId);
    }
}
