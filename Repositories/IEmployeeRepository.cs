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
        Task<List<Employee>> SearchEmployees(string name);
        Task<List<Employee>> FilterEmployeesByDepartment(string department);
        Task<List<Employee>> SortEmployeesBySalary(string order);
    }
}