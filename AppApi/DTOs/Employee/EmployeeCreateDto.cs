using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AppApi.DTOs.Employee
{
    public class EmployeeCreateDto
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public IFormFile UploadImage { get; set; }
    }

    public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeCreateDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().NotEmpty().WithMessage("Can't be empty");
            RuleFor(x => x.Address).MaximumLength(30).WithMessage("Max length can be 30");
        }
    }
}
