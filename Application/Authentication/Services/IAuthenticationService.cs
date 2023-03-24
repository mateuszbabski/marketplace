namespace Application.Authentication.Services
{
    public interface IAuthenticationService
    {        
        Task<AuthenticationResult> LoginCustomer(LoginRequest request);
        Task<AuthenticationResult> LoginShop(LoginRequest request);
        Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request);
        Task<AuthenticationResult> RegisterShop(RegisterShopRequest request);
    }
}
