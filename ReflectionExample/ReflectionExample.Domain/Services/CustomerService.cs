using ReflectionExample.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace ReflectionExample.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        public Dictionary<string, int> GetCustomerData()
        => new() { ["Pesho"] = 20, ["Ivan"] = 28, ["Gosho"] = 34 };
    }
}
