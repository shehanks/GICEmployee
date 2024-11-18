using GICEmployee.Application.Dtos;
using GICEmployee.Application.Features.Cafe.Queries;
using GICEmployee.Application.Interfaces;
using MediatR;
using System.Linq.Expressions;
using Entity = GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Features.Cafe.Handlers
{
    public class GetCafesByLocationQueryHandler : IRequestHandler<GetCafesByLocationQuery, IEnumerable<GetCafeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCafesByLocationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetCafeDto>> Handle(GetCafesByLocationQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Entity.Cafe, bool>>? filter = null;

            if (!string.IsNullOrWhiteSpace(request.Location))
                filter = cafe => cafe.Location.ToLower().StartsWith(request.Location.ToLower());

            var sortedCafes = await _unitOfWork.CafeRepository.GetSelectAsync(
                    filter: filter,
                    orderBy: query => query.OrderByDescending(c => c.EmployeeCafeRelationships!.Count),
                    selector: query => query.Select(c => new GetCafeDto()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        EmployeeCount = c.EmployeeCafeRelationships!.Count,
                        Location = c.Location,
                        ImageBase64 = c.Logo != null ? Convert.ToBase64String(c.Logo) : null
                    }));

            return sortedCafes.Any() ? sortedCafes : Enumerable.Empty<GetCafeDto>();
        }
    }
}
