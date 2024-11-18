using GICEmployee.Common.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest<Entity.Employee>
    {
        [RegularExpression(@"UI[A-Za-z0-9]{7}", ErrorMessage = "Invalid ID format")]
        [MaxLength(9)]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9_+&*-]+(?:\.[a-zA-Z0-9_+&*-]+)*@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Invalid email address format.")]
        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(320, ErrorMessage = "Email must not exceed 320 characters.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[89][0-9]{7}$", ErrorMessage = "Invalid phone number format")]
        [MaxLength(8, ErrorMessage = "Phone number must not exceed 8 characters.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [Range(1, 2, ErrorMessage = "Please select a valid gender.")]
        public Gender Gender { get; set; }

        public Guid? CafeId { get; set; }
    }
}
