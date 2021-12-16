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
        }

        public Customer? GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(customer => customer.Id == id);
        }

        public Customer? Insert(Customer customer)
        {
            int maxId = 0;

            _customers.ForEach(customer => maxId = customer.Id > maxId ? customer.Id : maxId);

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
