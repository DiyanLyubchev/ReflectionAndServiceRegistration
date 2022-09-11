using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ReflectionExample.Domain.Interfaces.Services;
using ReflectionExample.Domain.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ReflectionExample.Api.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHostingEnvironment environment;

        /// <summary>
        /// Register all services with Reflection
        /// </summary>
        private readonly ICustomerService customerService;

        public HomeController(IHostingEnvironment environment, ICustomerService customerService)
        {
            this.environment = environment;
            this.customerService = customerService;
        }


        [HttpGet("get")]
        public IActionResult Get()
        {
            Dictionary<string, int> names = this.customerService.GetCustomerData();

            GetMethodsTest();

            ReflectionTest();

            var data = names.Select(u => new
            {
                Name = u.Key,
                Age = u.Value
            });

            return Ok(data);
        }

        private static void GetMethodsTest()
        {
            Type type = typeof(InfrastructureManager);
            InfrastructureManager service = (InfrastructureManager)Activator.CreateInstance(type, "http://www.google.com/");
            foreach (var method in type.GetMethods())
            {
                string currentMethod = method.Name;

                foreach (var parameter in method.GetParameters())
                {
                    string currentprm = parameter.Name;
                }

                foreach (var attribute in method.GetCustomAttributes())
                {
                    string att = attribute.GetType().Name;
                }
            }
        }

        private object ReflectionTest()
        {
            string path = Path.Combine(this.environment.ContentRootPath + "\\MyDll", "ReflectionExample.TestClassLibrary.dll");
            Assembly assembly = Assembly.LoadFrom(path);
            Type type = assembly.GetType("ReflectionExample.TestClassLibrary.InfrastructureService");

            var instance = Activator.CreateInstance(type, "http://www.google.com/");

            object data = instance.GetType().GetMethod("GetWebSiteStringResponse").Invoke(instance, null);

            return data;
        }
    }
}
