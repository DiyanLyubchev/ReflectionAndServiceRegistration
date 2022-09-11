using ReflectionExample.Domain.Interfaces.LifetimeRegistrationOptions;
using System.Collections.Generic;

namespace ReflectionExample.Domain.Interfaces.Services
{
    public interface ICustomerService : ITransientService
    {
        Dictionary<string, int> GetCustomerData();
    }
}
