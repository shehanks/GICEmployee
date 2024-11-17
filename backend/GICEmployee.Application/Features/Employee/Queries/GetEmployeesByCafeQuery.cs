using GICEmployee.Application.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GICEmployee.Application.Features.Cafe.Queries
{
    public class GetEmployeesByCafeQuery : IRequest<IEnumerable<GetEmployeeDto>>
    {
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string? Cafe { get; set; }

        public GetEmployeesByCafeQuery(string? cafe)
        {
            Cafe = cafe;
        }
    }
}
