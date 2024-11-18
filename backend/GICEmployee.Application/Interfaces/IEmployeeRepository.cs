using GICEmployee.Domain.Entities;

namespace GICEmployee.Application.Interfaces
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        // Add method definitions of repository specific methods if required.
        // Override base repository methods if required.

        Task<Employee?> GetEmployeeByIdWithCafeAsync(string id, CancellationToken cancellationToken);

        Task<IEnumerable<Employee>> GetEmployeesByCafeIdAsync(Guid cafeId);
    }
}
