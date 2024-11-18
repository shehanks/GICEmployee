using GICEmployee.Application.Features.Cafe.Commands;
using GICEmployee.Application.Interfaces;
using MediatR;

namespace GICEmployee.Application.Features.Cafe.Handlers
{
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCafeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _unitOfWork.CafeRepository.GetByIdAsync(request.Id);

            if (cafe == null)
                throw new KeyNotFoundException("Cafe not found.");

            // Retrieve all employees under this cafe
            var employees = await _unitOfWork.EmployeeRepository.GetEmployeesByCafeIdAsync(request.Id);

            // Delete all employees associated with the cafe
            foreach (var employee in employees)
            {
                _unitOfWork.EmployeeRepository.Delete(employee);
            }

            // Delete the cafe itself
            _unitOfWork.CafeRepository.Delete(cafe);

            await _unitOfWork.CompleteAsync();
        }
    }
}
