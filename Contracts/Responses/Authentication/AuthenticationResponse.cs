using Domain.Entities;

namespace Contracts.Responses.Authentication
{
    public record AuthenticationResponse
    (
        Customer Customer,
        string Token
    );
}
