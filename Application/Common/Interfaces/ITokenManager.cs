﻿namespace Application.Common.Interfaces
{
    public interface ITokenManager
    {
        string GenerateToken(Guid id, string email);
    }
}
