using GICEmployee.Application.Interfaces;
using GICEmployee.Domain.Entities;
using GICEmployee.Infrastructure.Persistence;

namespace GICEmployee.Infrastructure.Repositories
{
    public class CafeRepository : RepositoryBase<Cafe>, ICafeRepository
    {
        public CafeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
