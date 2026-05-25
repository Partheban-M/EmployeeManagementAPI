using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(int id, Employee employee);
        Task<bool> DeleteEmployee(int id);
    }
}