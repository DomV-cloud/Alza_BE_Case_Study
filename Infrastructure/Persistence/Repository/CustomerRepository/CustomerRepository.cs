using Application.Interfaces.Repository.CustomerRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AlzaDbContext _context;

        public CustomerRepository(AlzaDbContext context)
        {
            _context = context;
        }

        public Customer? GetCustomerByCustomerName(string customerName)
        {
            var customer = _context.Customers.FirstOrDefault(
                u => u.CustomerName.Equals(customerName));

            return customer;
        }
    }
}
