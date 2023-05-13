using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }
        Guid UserId { get; }
        
    }
}
