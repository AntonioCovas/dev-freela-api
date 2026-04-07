using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, string role);
        public string HashPassword(string password);
        public bool VerifyPassword(string hashedPassword, string providedPassword);
        string ComputeSha256Hash(string password);
    }
}
