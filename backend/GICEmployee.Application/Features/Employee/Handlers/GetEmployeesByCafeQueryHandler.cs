using GICEmployee.Application.Dtos;
using GICEmployee.Application.Features.Cafe.Queries;
using GICEmployee.Application.Interfaces;
using MediatR;
using System.Linq.Expressions;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Cafe.Handlers
{
    public class GetEmployeesByCafeQueryHandler : IRequestHandler<GetEmployeesByCafeQuery, IEnumerable<GetEmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployeesByCafeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetEmployeeDto>> Handle(GetEmployeesByCafeQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Now;
            Expression<Func<Entity.Employee, bool>>? filter = null;

            if ((request.CafeId != null))
                filter = employee
                    => employee.EmployeeCafeRelationship != null && employee.EmployeeCafeRelationship.Cafe!.Id == request.CafeId;

            var sortedEmployees = await _unitOfWork.EmployeeRepository.GetSelectAsync(
                    filter: filter,
                    orderBy: query => query.OrderBy(c => c.EmployeeCafeRelationship != null && c.EmployeeCafeRelationship!.StartDate.HasValue ? c.EmployeeCafeRelationship!.StartDate : today.AddDays(1)),
                    selector: query => query.Select(x => new GetEmployeeDto()
                    {
                        Id = x.Id,
                        CafeName = x.EmployeeCafeRelationship!.Cafe!.Name,
                        Name = x.Name,
                        DaysWorked = x.EmployeeCafeRelationship!.StartDate.HasValue ? Math.Abs((x.EmployeeCafeRelationship!.StartDate.Value - today).Days) : 0,
                        EmailAddress = x.EmailAddress,
                        PhoneNumber = x.PhoneNumber
                    }));

            return sortedEmployees.Any() ? sortedEmployees : Enumerable.Empty<GetEmployeeDto>();
        }
    }
}
