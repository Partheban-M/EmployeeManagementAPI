namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeDto
    {
        public string Name { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public decimal Salary { get; set; }
    }
}