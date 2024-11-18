using MediatR;

namespace GICEmployee.Application.Features.Cafe.Commands
{
    public class DeleteCafeCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
