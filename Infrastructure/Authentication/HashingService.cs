using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class HashingService : IHashingService
    {
        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(8);
        }

        public string GenerateHashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
