using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace GenAI_Workshop_Assessment_2025.Services
{
    public interface IAuthService
    {
        string Login(string username, string password);
        void Logout(string token);
        ClaimsPrincipal? ValidateToken(string token);
    }
}