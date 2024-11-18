using GICEmployee.Application.Interfaces;
using GICEmployee.Domain.Entities;
using GICEmployee.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GICEmployee.Infrastructure.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetEmployeeByIdWithCafeAsync(string employeeId, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees
                .Include(e => e.EmployeeCafeRelationship) // Including related EmployeeCafeRelationship
                .FirstOrDefaultAsync(e => e.Id == employeeId, cancellationToken);

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCafeIdAsync(Guid cafeId)
        {
            return await dbContext.Employees
                .Where(e => e.EmployeeCafeRelationship != null && e.EmployeeCafeRelationship.CafeId == cafeId)
                .ToListAsync();
        }
    }
}
