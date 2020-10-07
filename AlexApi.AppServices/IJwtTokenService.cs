using System;
using System.Collections.Generic;
using System.Text;

namespace AlexApi.AppServices
{
    public interface IJwtTokenService
    {
        string GenerateJWTToken(string userName);
    }
}
