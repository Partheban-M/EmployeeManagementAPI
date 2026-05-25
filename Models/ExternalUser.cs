namespace EmployeeManagementAPI.Models
{
    public class ExternalUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
    }
}