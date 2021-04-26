using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tarea3.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private NorthWindContext dbcontext;

        private readonly ILogger<CustomersController> _logger;

        public ProductsController(ILogger<CustomersController> logger)
        {
            this.dbcontext = new NorthWindContext();
            _logger = logger;
        }

        [HttpGet]
        [Route("/Products")]
        public List<Product> GetAll()
        {
            return this.dbcontext.Products.ToList();
        }

        [HttpGet]
        [Route("/Products/{id}")]
        public Object Get(int id)
        {
            return this.dbcontext.Products.Where(Product => Product.ProductId == id).FirstOrDefault();

        }

        [HttpPut("/Products")]
        public string Put(Product pdc)
        {
            
            try{
                dbcontext.Products.Add(pdc);
                dbcontext.SaveChanges();
                return "Ok";
            }catch(Exception e){
                return "An error ocurred.";
            }

        }

        [HttpDelete("/Products/{id}")]
        public string Delete(int id)
        {
            
            Product pdc = this.dbcontext.Products.Where(Product => Product.ProductId == id).FirstOrDefault();
            
            if(pdc != null){
                dbcontext.Remove(pdc);
                dbcontext.SaveChanges();
                return "Product removed";
            }else{
                return "Product does not exist.";
            }

        }

        [HttpPost("/Products")]
        public string Post(Product pdc)
        {
            
            try{
                dbcontext.Products.Update(pdc);
                dbcontext.SaveChanges();
                return "Ok";
            }catch(Exception e){
                return "An error ocurred.";
            }

        }



    }
}
