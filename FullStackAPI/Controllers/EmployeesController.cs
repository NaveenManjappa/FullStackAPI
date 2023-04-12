using FullStackAPI.Data;
using FullStackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackAPI.Controllers
{
    [ApiController]
    [Route("/api/employees")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext fullStackDbContext;

        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            this.fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees=await fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employee)
        {
            //Random rand = new Random();
            //employee.Id = rand.Next(4, 100);
           await fullStackDbContext.Employees.AddAsync(employee);
            await fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
           var employee= await fullStackDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
                return NotFound();
            return Ok(employee);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id,Employee employee)
        {
            var emp= await fullStackDbContext.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            emp.Phone = employee.Phone;
            emp.Salary = employee.Salary;
            emp.Department = employee.Department;
            await fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var emp = await fullStackDbContext.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();
             fullStackDbContext.Employees.Remove(emp);
            await fullStackDbContext.SaveChangesAsync();
            return Ok(emp);
        }
    }
}
