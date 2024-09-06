using Application.Interfaces.Authentication;
using Application.Interfaces.Repository.CustomerRepository;
using Contracts.Responses.Authentication;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICustomerRepository _customerRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, ICustomerRepository customerRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _customerRepository = customerRepository;
        }

        public AuthenticationResponse? Login(string customerName, string password)
        {
            var customer = _customerRepository.GetCustomerByCustomerName(customerName) ?? throw new Exception("User not found");
         
            if (customer.CustomerName != customerName)
            {
                return null;
                throw new Exception("Invalid Customer name");
            }

            if (customer.Password != password)
            {
                return null;
                throw new Exception("Invalid password");
            }

            var token = _jwtTokenGenerator.GenerateToken(customer);

            return new AuthenticationResponse(
              customer,
              token);
        }

        public AuthenticationResponse Register(string customerName, string? customerSurname, string password)
        {
            throw new NotImplementedException();
        }
    }
}
