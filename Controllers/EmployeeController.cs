using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EmployeeManagementAPI.DTOs;
namespace EmployeeManagementAPI.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }
}

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService,ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeService = employeeService;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
             _logger.LogInformation("Fetching all employees");
            var employees = await _employeeService.GetAllEmployees();
            var result = _mapper.Map<List<EmployeeDto>>(employees);
            return Ok(new ApiResponse<List<EmployeeDto>>
            {
                Success = true,
                Message = "Employees fetched successfully",
                Data = result
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound(new ApiResponse<EmployeeDto>
                {
                    Success = false,
                    Message = "Employee not found",
                    Data = null
               });
            }
            var result = _mapper.Map<EmployeeDto>(employee);
            return Ok(new ApiResponse<EmployeeDto>
            {
                Success = true,
                Message = "Employee fetched successfully",
                Data = result
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            var createdEmployee = await _employeeService.AddEmployee(employee);

            var result = _mapper.Map<EmployeeDto>(createdEmployee);

            return Ok(new ApiResponse<EmployeeDto>
            {
                Success = true,
                Message = "Employee added successfully",
                Data = result
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            var updatedEmployee = await _employeeService.UpdateEmployee(id, employee);

            if (updatedEmployee == null)
            {
                return NotFound(new ApiResponse<EmployeeDto>
                {
                    Success = false,
                    Message = "Employee not found",
                    Data = null
                });
            }

            var result = _mapper.Map<EmployeeDto>(updatedEmployee);

            return Ok(new ApiResponse<EmployeeDto>
            {
                Success = true,
                Message = "Employee updated successfully",
                Data = result
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            if (!result)
            {
                return NotFound(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Employee not found",
                    Data = null
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Employee deleted successfully",
                Data = null
            });
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
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var filePath = Path.Combine(folderPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(new
            {
                message = "File uploaded successfully",fileName = file.FileName
            });
        }    
    }
}