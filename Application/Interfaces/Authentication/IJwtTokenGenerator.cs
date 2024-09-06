using Domain.Entities;

namespace Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Customer customer);
    }
}
