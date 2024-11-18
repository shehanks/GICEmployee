using GICEmployee.Application.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GICEmployee.Application.Features.Cafe.Queries
{
    public class GetEmployeesByCafeQuery : IRequest<IEnumerable<GetEmployeeDto>>
    {
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public Guid? CafeId { get; set; }

        public GetEmployeesByCafeQuery(Guid? cafe)
        {
            CafeId = cafe;
        }
    }
}
