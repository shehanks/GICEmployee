namespace GICEmployee.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }

        ICafeRepository CafeRepository { get; }

        Task<int> CompleteAsync();
    }
}
