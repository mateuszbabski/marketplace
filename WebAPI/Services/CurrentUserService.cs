using Application.Common.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{   

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public ClaimsPrincipal User => _httpContext.HttpContext?.User;

        public Guid UserId => Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
