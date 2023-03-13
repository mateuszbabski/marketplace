using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IHashingService
    {
        string GenerateSalt();
        string GenerateHashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
