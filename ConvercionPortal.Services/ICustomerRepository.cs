using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers(Dictionary<string, string> filter);

        Customer? GetCustomerById(int id);

        bool Update(Customer customer);

        bool Delete(int id);

        Customer? Insert(Customer customer);
    }
}
