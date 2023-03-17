namespace Application.Authentication.Services
{
    public interface IAuthenticationService
    {        
        Task<AuthenticationResult> Login(LoginRequest request);
        Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request);
        //AuthenticationResult RegisterEnterpreneur();
    }
}
