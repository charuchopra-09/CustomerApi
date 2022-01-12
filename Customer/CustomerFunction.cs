using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;

namespace Customer
{
    public static class CustomerFunction
    {
        public static readonly List<Customer> customers = new List<Customer>();

        [FunctionName("CreateCustomer")]
        public static async Task<IActionResult> CreateCustomer(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "customer")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation(message: "Creating a new Customer");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(requestBody);
            
           // var customer = new Customer() { Name = input.Name, Country = input.Country, PhoneNumber = input.PhoneNumber };
            customers.Add(customer);
            return new OkObjectResult(customer);
        }

        [FunctionName("GetAllCustomers")]
        public static async Task<IActionResult> GetAllCustomers(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer")] HttpRequest req,
           ILogger log)
        {
            log.LogInformation(message: "Getting all the customer list");
            return new OkObjectResult(customers);
        }

        [FunctionName("GetCustomerById")]
        public static async Task<IActionResult> GetCustomerById(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "customer/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            var customer = customers.FirstOrDefault(t => t.Id == id);
            if(customer == null)
            {
                return new NotFoundObjectResult(null);
            }
            return new OkObjectResult(customer);
        }
    }
}
