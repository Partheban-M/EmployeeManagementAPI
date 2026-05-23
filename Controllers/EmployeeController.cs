using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();

            return Ok(employees);
        }

        // GET: api/Employee/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // PUT: api/Employee/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updatedEmployee)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            employee.Name = updatedEmployee.Name;
            employee.Department = updatedEmployee.Department;
            employee.Salary = updatedEmployee.Salary;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // DELETE: api/Employee/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return Ok("Employee deleted successfully");
        }
    }
}