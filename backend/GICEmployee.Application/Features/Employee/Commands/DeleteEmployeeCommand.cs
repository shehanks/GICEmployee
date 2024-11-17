using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GICEmployee.Application.Features.Employee.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        [RegularExpression(@"UI[A-Za-z0-9]{7}", ErrorMessage = "Invalid ID format")]
        [MaxLength(9)]
        public string Id { get; set; } = string.Empty;
    }
}
