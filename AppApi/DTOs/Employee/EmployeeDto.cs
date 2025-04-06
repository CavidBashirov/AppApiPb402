using FluentValidation;

namespace AppApi.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CreateDate { get; set; }
        public string Image { get; set; }
    }

}
