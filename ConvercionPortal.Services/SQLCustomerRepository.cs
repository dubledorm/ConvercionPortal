using ConvercionPortal.Models;
using System.Linq.Expressions;

namespace ConvercionPortal.Services
{
    public class SQLCustomerRepository: ScopedRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;

        public SQLCustomerRepository(AppDbContext context) : base()
        {
            _context = context;
            AddScope("Name", ScopeByName);
            AddScope("Description", ScopeByDescription);
        }

        // TODO. Возможно, что как-то можно не дублировать здесь функцию определённую в базовом классе.
        public void AddScopeRalation(string ScopeName, Func<string> PropertyGetter)
        {
            base.AddScopeRelation(ScopeName, PropertyGetter);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Customer> GetAllCustomers()
        {
            var query = (from customer in _context.Customers.AsQueryable() select customer);
            query = ExtendQueryByFilter(query);

            var result = from customer in query select customer;

            return result;
        }

        public Customer? GetCustomerById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer? Insert(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            var _customer = _context.Customers.Attach(customer);

            if (_customer == null)
                return false;

            _customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return true;
        }


        public IQueryable<Customer> ScopeByName(IQueryable<Customer> query, string value)
        {
            query = query.Where(customer => customer.Name.ToLower().Contains(value.ToLower()));
            return query;
        }

        public IQueryable<Customer> ScopeByDescription(IQueryable<Customer> query, string value)
        {
            query = query.Where(customer => customer.Description.ToLower().Contains(value.ToLower()));
            return query;
        }
    }
}
