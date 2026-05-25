using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeeById(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(int id, Employee employee);
        Task<bool> DeleteEmployee(int id);
        Task<List<Employee>> GetEmployeesPaginated(
            int pageNumber,
            int pageSize
        );
    }
}