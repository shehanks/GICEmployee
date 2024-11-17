using GICEmployee.Application.Features.Cafe.Handlers;
using GICEmployee.Application.Features.Employee.Handlers;
using GICEmployee.Application.Interfaces;
using GICEmployee.Application.Interfaces.Services;
using GICEmployee.Application.Services;
using GICEmployee.Infrastructure.Persistence;
using GICEmployee.Infrastructure.Repositories;
using GICEmployee.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace GICEmployee.API
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with the connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            // Register DatabaseInitializer
            services.AddScoped<DatabaseInitializer>();

            // Register repositories here
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICafeRepository, CafeRepository>();

            services.AddScoped<IEmployeeIdGeneratorService, EmployeeIdGeneratorService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCafeCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCafesByLocationQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEmployeesByCafeQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateCafeCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateEmployeeCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteEmployeeCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteCafeCommandHandler).Assembly));

            // Register unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
