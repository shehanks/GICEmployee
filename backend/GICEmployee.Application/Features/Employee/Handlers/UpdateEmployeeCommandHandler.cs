using GICEmployee.Application.Features.Employee.Commands;
using GICEmployee.Application.Interfaces;
using GICEmployee.Domain.Entities;
using MediatR;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Employee.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Entity.Employee>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Entity.Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the existing employee
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdWithCafeAsync(request.Id, cancellationToken);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found.");

            // Update the relationship with the cafe
            if (request.CafeId != null)
            {
                var cafe = await _unitOfWork.CafeRepository.GetByIdAsync(request.CafeId);
                var today = DateTime.Now;

                if (cafe == null)
                    throw new KeyNotFoundException("Cafe not found.");

                // If the employee already has a relationship, update it
                if (employee.EmployeeCafeRelationship == null ||
                    (employee.EmployeeCafeRelationship != null && employee.EmployeeCafeRelationship.CafeId != cafe.Id))
                {
                    employee.EmployeeCafeRelationship = null;

                    var employeeCafe = new EmployeeCafeRelationship
                    {
                        CafeId = cafe.Id,
                        EmployeeId = request.Id,
                        StartDate = today
                    };

                    employee.EmployeeCafeRelationship = employeeCafe;
                }
            }
            else
            {
                // If no cafe is provided, remove the existing relationship (if any)
                employee.EmployeeCafeRelationship = null;
            }

            // Update the employee details
            employee.Name = request.Name;
            employee.EmailAddress = request.EmailAddress;
            employee.PhoneNumber = request.PhoneNumber;
            employee.Gender = request.Gender;

            // Update the employee in the repository
            _unitOfWork.EmployeeRepository.Update(employee);
            await _unitOfWork.CompleteAsync();

            return employee;
        }
    }
}
