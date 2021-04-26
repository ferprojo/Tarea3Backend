using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tarea3.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private NorthWindContext dbcontext;

        private readonly ILogger<CustomersController> _logger;

        public EmployeesController(ILogger<CustomersController> logger)
        {
            this.dbcontext = new NorthWindContext();
            _logger = logger;
        }

        //Por lo pronto este endpoint dara un error,
        //por que el campo de Foto en la DB es bastante largo
        [HttpGet]
        [Route("/Employees")]
        public List<Employee> GetAll()
        {
            return this.dbcontext.Employees.ToList();
        }

        [HttpGet]
        [Route("/Employees/{id}")]
        public Object Get(int id)
        {
            return this.dbcontext.Employees.Where(Employee => Employee.EmployeeId == id).FirstOrDefault();

        }

        [HttpPut("/Employees")]
        public string Put(Employee emp)
        {
            
            try{
                dbcontext.Employees.Add(emp);
                dbcontext.SaveChanges();
                return "Ok";
            }catch(Exception e){
                return e.ToString();
            }

        }


        //Dara error si intentas borrar un empleado
        //Que sea referenciado en otro campo en la DB.
        //Para probarlo, crear otro empleado.
        [HttpDelete("/Employees/{id}")]
        public string Delete(int id)
        {
            
            Employee emp = this.dbcontext.Employees.Where(Employee => Employee.EmployeeId == id).FirstOrDefault();
            
            if(emp != null){
                dbcontext.Remove(emp);
                dbcontext.SaveChanges();
                return "Employee removed";
            }else{
                return "Employee does not exist.";
            }

        }

        [HttpPost("/Employees")]
        public string Post(Employee emp)
        {
            
            try{
                dbcontext.Employees.Update(emp);
                dbcontext.SaveChanges();
                return "Ok";
            }catch(Exception e){
                return "An error ocurred.";
            }

        }



    }
}
