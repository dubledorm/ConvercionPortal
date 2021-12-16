using ConvercionPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer? GetCustomerById(int id);
    }
}
