namespace GICEmployee.Application.Interfaces.Services
{
    public interface IEmployeeIdGeneratorService
    {
        Task<string> GenerateUniqueEmployeeIdAsync(CancellationToken cancellationToken = default);
    }
}
