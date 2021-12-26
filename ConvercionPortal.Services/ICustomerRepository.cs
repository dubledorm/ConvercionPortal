using ConvercionPortal.Models;

namespace ConvercionPortal.Services
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer? GetCustomerById(int id);

        bool Update(Customer customer);

        bool Delete(int id);

        Customer? Insert(Customer customer);

        void AddScopeRalation(string ScopeName, Func<string> PropertyGetter);
    }
}
