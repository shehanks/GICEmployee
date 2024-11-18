using GICEmployee.Application.Interfaces;
using GICEmployee.Application.Interfaces.Services;
using GICEmployee.Common.Helpers;

namespace GICEmployee.Application.Services
{
    public class EmployeeIdGeneratorService : IEmployeeIdGeneratorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeIdGeneratorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateUniqueEmployeeIdAsync(CancellationToken cancellationToken = default)
        {
            string newId = string.Empty;
            bool isUnique = false;

            do
            {
                newId = UniqueIdHelper.GenerateEmployeeId();
                isUnique = !await _unitOfWork.EmployeeRepository.ExistsAsync(x => string.Equals(x.Id, newId), cancellationToken);

            } while (!isUnique);

            return newId;
        }
    }
}
