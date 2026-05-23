using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
    }
}