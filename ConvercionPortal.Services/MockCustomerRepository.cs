using ConvercionPortal.Models;
using Microsoft.Extensions.Logging;

namespace ConvercionPortal.Services
{
    public class MockCustomerRepository : ScopedRepository<Customer>, ICustomerRepository
    {
        private List<Customer> _customers;

        public MockCustomerRepository(ILogger<ScopedRepository<Customer>> logger): base()
        {
            _customers = new List<Customer>()
             {
                new Customer()
                  { Id = 1, Name = "Рога и копыта"},
                new Customer()
                  { Id = 2, Name = "Бешенный огурец", Description = "skd sad ,nd ,ANSAD "},
                new Customer()
                  { Id = 3, Name = "Смерть и дева"}
             };
        }

        public void AddScopeRalation(string ScopeName, Func<string> PropertyGetter)
        {
            base.AddScopeRelation(ScopeName, PropertyGetter);
        }

        public bool Delete(int id)
        {
            Customer? customerForDelete = GetCustomerById(id);

            if (customerForDelete == null)
                return false;


            return _customers.Remove(customerForDelete);
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customers;
            //   if (filter.Count == 0)
            //       return _customers;

            //    MockRepositoryFilter mockRepositoryFilter = new(filter);

            //    var result = from customer in _customers 
            //                 where mockRepositoryFilter.Compare(customer) //фильтрация по критерию
            //                 select customer; // выбираем объект
            //    return result;
        }

        public Customer? GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(customer => customer.Id == id);
        }

        public Customer? Insert(Customer customer)
        {
            int maxId = _customers.Max<Customer>(itemCustomer => itemCustomer.Id);

            customer.Id = maxId + 1;
            _customers.Add(customer);
            return customer;
        }

        public bool Update(Customer customer)
        {
            Customer? customerForUpdate = GetCustomerById(customer.Id);

            if (customerForUpdate == null)
                return false;

            // Копирование
            customerForUpdate.Name = customer.Name;
            customerForUpdate.Description = customer.Description;

            return true;
        }
    }
}
