using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assingment_nov25
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int id);
    }
    public class CustomerService
    {
        private readonly ICustomerRepository repo;
            public CustomerService(ICustomerRepository repo)
        {
            repo = repo;
        }
    }
}
