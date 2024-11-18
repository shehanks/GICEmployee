using MediatR;
using System.ComponentModel.DataAnnotations;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Cafe.Commands
{
    public class UpdateCafeCommand : IRequest<Entity.Cafe>
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description must not exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(200, ErrorMessage = "Location must not exceed 200 characters.")]
        public string Location { get; set; } = string.Empty;

        public string? ImageBase64 { get; set; }
    }
}
