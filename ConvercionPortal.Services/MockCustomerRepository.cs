using ConvercionPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    public class MockCustomerRepository : ICustomerRepository
    {
        private List<Customer> _customers;

        public MockCustomerRepository()
        {  _customers = new List<Customer>()
             {
                new Customer() 
                  { Id = 1, Name = "Рога и копыта"}, 
                new Customer()
                  { Id = 2, Name = "Бешенный огурец", Description = "skd sad ,nd ,ANSAD "},
                new Customer()
                  { Id = 3, Name = "Смерть и дева"}
             };
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer? GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(customer => customer.Id == id);
        }
    }
}
