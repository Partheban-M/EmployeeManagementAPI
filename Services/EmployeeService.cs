using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await _employeeRepository.AddEmployee(employee);
        }

        public async Task<Employee?> UpdateEmployee(int id, Employee employee)
        {
            return await _employeeRepository.UpdateEmployee(id, employee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }
        public async Task<List<Employee>> SearchEmployees(string name)
        {
            return await _employeeRepository.SearchEmployees(name);
        }
        public async Task<List<Employee>> GetEmployeesPaginated(
            int pageNumber,
            int pageSize)
        {
            return await _employeeRepository.GetEmployeesPaginated(pageNumber, pageSize);
        }
    }
}