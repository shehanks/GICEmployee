using GICEmployee.Application.Features.Employee.Commands;
using GICEmployee.Application.Interfaces;
using GICEmployee.Application.Interfaces.Services;
using GICEmployee.Domain.Entities;
using MediatR;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Employee.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Entity.Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEmployeeIdGeneratorService _employeeIdGeneratorService;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, IEmployeeIdGeneratorService employeeIdGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _employeeIdGeneratorService = employeeIdGeneratorService;
        }

        public async Task<Entity.Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Generate a unique employee ID
            var uniqueId = await _employeeIdGeneratorService.GenerateUniqueEmployeeIdAsync(cancellationToken);

            var employee = new Entity.Employee
            {
                Id = uniqueId,
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender
            };

            if (request.CafeId != null)
            {
                var employeCafe = new EmployeeCafeRelationship
                {
                    CafeId = request.CafeId.Value,
                    EmployeeId = uniqueId,
                    StartDate = DateTime.Now,
                };

                employee.EmployeeCafeRelationship = employeCafe;
            }

            await _unitOfWork.EmployeeRepository.InsertAsync(employee);
            await _unitOfWork.CompleteAsync();

            return employee;
        }
    }
}
