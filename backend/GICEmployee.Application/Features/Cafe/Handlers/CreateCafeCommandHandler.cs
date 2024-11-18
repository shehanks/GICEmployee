using GICEmployee.Application.Features.Cafe.Commands;
using GICEmployee.Application.Interfaces;
using MediatR;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Cafe.Handlers
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Entity.Cafe>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCafeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Entity.Cafe> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Entity.Cafe
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
            };

            if (!string.IsNullOrEmpty(request.ImageBase64))
                cafe.Logo = Convert.FromBase64String(request.ImageBase64);

            await _unitOfWork.CafeRepository.InsertAsync(cafe);
            await _unitOfWork.CompleteAsync();

            return cafe;
        }
    }
}
