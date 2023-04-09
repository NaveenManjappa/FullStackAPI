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
    }
}
