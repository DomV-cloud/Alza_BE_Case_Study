using Domain.Entities;

namespace Application.Interfaces.Repository.CustomerRepository
{
    public interface ICustomerRepository
    {
        public Customer? GetCustomerByCustomerName(string customerName);
    }
}
