using GICEmployee.Application.Dtos;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GICEmployee.Application.Features.Cafe.Queries
{
    public class GetCafesByLocationQuery : IRequest<IEnumerable<GetCafeDto>>
    {
        [MaxLength(200, ErrorMessage = "Location must not exceed 200 characters.")]
        public string? Location { get; set; }

        public GetCafesByLocationQuery(string? location)
        {
            Location = location;
        }
    }
}
