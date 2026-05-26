using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService,ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation("Fetching all employees");

            var employees = await _employeeService.GetAllEmployees();

            return Ok(employees);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var createdEmployee = await _employeeService.AddEmployee(employee);
            return Ok(createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(id, employee);

            if (updatedEmployee == null)
            {
                return NotFound("Employee not found");
            }

            return Ok(updatedEmployee);
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            if (!result)
            {
                return NotFound("Employee not found");
            }

            return Ok("Employee deleted successfully");
        }
        [HttpGet("paginated")]
        public async Task<IActionResult> GetEmployeesPaginated(
            int pageNumber = 1,
            int pageSize = 10)
        {
            var employees = await _employeeService.GetEmployeesPaginated(pageNumber, pageSize);
            return Ok(employees);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(string name)
        {
            _logger.LogInformation("Searching employees with name: {Name}", name);
            var employees = await _employeeService.SearchEmployees(name);
            return Ok(employees);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> FilterEmployeesByDepartment(string department)
        {
            _logger.LogInformation("Filtering employees by department: {Department}", department);
            var employees = await _employeeService.FilterEmployeesByDepartment(department);
            return Ok(employees);
        }
        [HttpGet("sort")]
        public async Task<IActionResult> SortEmployeesBySalary(string order)
        {
            _logger.LogInformation("Sorting employees by salary in {Order} order", order);
            var employees = await _employeeService.SortEmployeesBySalary(order);
            return Ok(employees);
        }
    }
}