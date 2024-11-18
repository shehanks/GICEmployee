using GICEmployee.Application.Features.Cafe.Commands;
using GICEmployee.Application.Interfaces;
using MediatR;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Cafe.Handlers
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, Entity.Cafe>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCafeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Entity.Cafe> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the cafe from the database
            var cafe = await _unitOfWork.CafeRepository.GetByIdAsync(request.Id);

            if (cafe == null)
                throw new KeyNotFoundException("Cafe not found.");

            cafe.Name = request.Name;
            cafe.Description = request.Description;
            cafe.Location = request.Location;

            if (!string.IsNullOrEmpty(request.ImageBase64))
                cafe.Logo = Convert.FromBase64String(request.ImageBase64);

            _unitOfWork.CafeRepository.Update(cafe);
            await _unitOfWork.CompleteAsync();

            return cafe;
        }
    }
}
