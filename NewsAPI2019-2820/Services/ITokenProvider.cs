using Microsoft.IdentityModel.Tokens;
using NewsAPI2019_2820.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPI2019_2820.Services
{
    public interface ITokenProvider
    {
        string CreateToken(User user, DateTime expirationDate);
        TokenValidationParameters GetValidationParameters();
    }
}
