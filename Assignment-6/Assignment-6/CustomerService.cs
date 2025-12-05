using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_6
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int id);
    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    internal class CustomerService
    {
        private readonly ICustomerRepository _repo;
        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public string GetCustomerName(int id)
        {
            var customer = _repo.GetCustomerById(id);
            return customer?.Name ?? "Unknown";
        }
    }
}