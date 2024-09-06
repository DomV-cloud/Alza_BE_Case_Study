using Application.Interfaces.Authentication;
using Application.Interfaces.Repository.CustomerRepository;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using Infrastructure.Services;

namespace UnitTests
{
    [TestClass]
    public class AuhtenticationServiceUnitTests
    {
        private Mock<ICustomerRepository> _customerRepositoryMock;
        private Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private AuthenticationService _authenticationService;

        public AuhtenticationServiceUnitTests()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _authenticationService = new AuthenticationService(_jwtTokenGeneratorMock.Object, _customerRepositoryMock.Object);
        }

        [TestCase("Aleš", "ZavoralJeBuhNaZemi")]
        public void Login_Should_Return_Customer(string customerName, string password)
        {
            var expectedCustomer = new Customer
            {
                CustomerName = customerName,
                Password = password
            };

            _customerRepositoryMock
                .Setup(x => x.GetCustomerByCustomerName(It.Is<string>(name => name == customerName)))
                .Returns(expectedCustomer);

            var token = "dummyToken";
            _jwtTokenGeneratorMock
                .Setup(x => x.GenerateToken(It.Is<Customer>(customer => customer.CustomerName == customerName)))
                .Returns(token);

            var authenticationService = new AuthenticationService(_jwtTokenGeneratorMock.Object, _customerRepositoryMock.Object);

            var result = authenticationService.Login(customerName, password);

            result.Should().NotBeNull();
            result.Customer.Should().Be(expectedCustomer);
            result.Token.Should().Be(token);
        }

        [Test]
        public void Login_UserNotFound_ThrowsException()
        {
            string customerName = "unknownUser";
            string password = "password";

            _customerRepositoryMock
                .Setup(repo => repo.GetCustomerByCustomerName(customerName))
                .Returns((Customer)null);

            Action act = () => _authenticationService.Login(customerName, password);

            act.Should().Throw<Exception>().WithMessage("User not found");
        }

        [Test]
        public void Login_InvalidCustomerName_ReturnsNull()
        {
            var customer = new Customer { CustomerName = "JohnDoe", Password = "password" };
            string customerName = "WrongUser";
            string password = "password";

            _customerRepositoryMock
                .Setup(repo => repo.GetCustomerByCustomerName(customerName))
                .Returns(customer);

            var result = _authenticationService.Login(customerName, password);

            result.Should().BeNull();
        }

        [Test]
        public void Login_InvalidPassword_ReturnsNull()
        {
            var customer = new Customer { CustomerName = "JohnDoe", Password = "password" };
            string customerName = "JohnDoe";
            string wrongPassword = "wrongPassword";

            _customerRepositoryMock
                .Setup(repo => repo.GetCustomerByCustomerName(customerName))
                .Returns(customer);

            var result = _authenticationService.Login(customerName, wrongPassword);

            result.Should().BeNull();
        }

        [Test]
        public void Login_ValidCredentials_ReturnsAuthenticationResponse()
        {
            var customer = new Customer { CustomerName = "John", CustomerSurname = "Doe", Password = "password" };
            string customerName = "JohnDoe";
            string password = "password";
            string token = "jwtToken";

            _customerRepositoryMock
                .Setup(repo => repo.GetCustomerByCustomerName(It.Is<string>(x => x == customerName)))
                .Returns(customer);

            _jwtTokenGeneratorMock
                .Setup(generator => generator.GenerateToken(It.Is<Customer>(x => x.CustomerName == customer.CustomerName && x.CustomerSurname == customer.CustomerSurname && x.Password == customer.Password)))
                .Returns(token);

            var result = _authenticationService.Login(customerName, password);

            result.Should().NotBeNull();
            result.Customer.Should().Be(customer);
            result.Token.Should().Be(token);
        }
    }
}