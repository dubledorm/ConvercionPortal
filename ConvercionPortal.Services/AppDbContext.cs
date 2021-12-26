using ConvercionPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ConvercionPortal.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
    }
}
