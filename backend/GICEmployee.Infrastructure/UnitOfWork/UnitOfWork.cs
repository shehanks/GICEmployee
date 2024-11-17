using GICEmployee.Application.Interfaces;
using GICEmployee.Infrastructure.Persistence;
using GICEmployee.Infrastructure.Repositories;

namespace GICEmployee.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        private readonly ApplicationDbContext _applicationDbContext;

        private IEmployeeRepository? _employeeRepository;

        private ICafeRepository? _cafeRepository;

        public IEmployeeRepository EmployeeRepository =>
            _employeeRepository ??= new EmployeeRepository(_applicationDbContext);

        public ICafeRepository CafeRepository =>
            _cafeRepository ??= (_cafeRepository = new CafeRepository(_applicationDbContext));

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext)); ;
        }

        public async Task<int> CompleteAsync() => await _applicationDbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    await _applicationDbContext.DisposeAsync();
            }
            disposed = true;
        }
    }
}
