namespace Contracts.Requests.Login
{
    public record LoginRequest
    (
        string CustomerName,
        string Password
    );
}
