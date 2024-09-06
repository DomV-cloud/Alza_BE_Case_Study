using Contracts.Responses.Authentication;

namespace Application.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResponse Register(string customerName, string? customerSurname, string password);
        AuthenticationResponse? Login(string customerName, string password);
    }
}
