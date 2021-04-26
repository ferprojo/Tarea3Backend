using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tarea3.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private NorthWindContext dbcontext;

        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            this.dbcontext = new NorthWindContext();
            _logger = logger;
        }

        [HttpGet]
        [Route("/Customers")]
        public List<Customer> GetAll()
        {
            return this.dbcontext.Customers.ToList();
        }

        [HttpGet]
        [Route("/Customers/{id}")]
        public Object Get(string id)
        {
            return this.dbcontext.Customers.Where(Customer => Customer.CustomerId == id).FirstOrDefault();

        }

        [HttpPut("/Customers")]
        public string Put(Customer cst)
        {
            
            try{
                dbcontext.Customers.Add(cst);
                dbcontext.SaveChanges();
                return "Ok";
            }catch(Exception e){
                return "An error ocurred.";
            }

        }

        [HttpDelete("/Customers/{id}")]
        public string Delete(string id)
        {
            
            Customer cust = this.dbcontext.Customers.Where(Customer => Customer.CustomerId == id).FirstOrDefault();
            
            if(cust != null){
                dbcontext.Remove(cust);
                dbcontext.SaveChanges();
                return "Customer removed";
            }else{
                return "Customer does not exist.";
            }

        }

        [HttpPost("/Customers")]
        public string Post(Customer cst)
        {
            
            try{
                dbcontext.Customers.Update(cst);
                dbcontext.SaveChanges();
                return "Ok";
            }catch(Exception e){
                return "An error ocurred.";
            }

        }



    }
}
