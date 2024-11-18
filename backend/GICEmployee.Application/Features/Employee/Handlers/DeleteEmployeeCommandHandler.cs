using GICEmployee.Application.Features.Employee.Commands;
using GICEmployee.Application.Interfaces;
using MediatR;

namespace GICEmployee.Application.Features.Employee.Handlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the employee from the repository
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(request.Id);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            // Delete the employee
            _unitOfWork.EmployeeRepository.Delete(employee);
            await _unitOfWork.CompleteAsync();
        }
    }
}
