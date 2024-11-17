using GICEmployee.API;
using GICEmployee.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add configuration based on environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Base settings
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true) // Environment-specific
    .AddEnvironmentVariables(); // Add environment variables

// Call the extension method to add database services
builder.Services.AddDatabaseServices(builder.Configuration);

// Add custom services to the container.
builder.Services.ConfigureServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Run the database initializer
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbInitializer = services.GetRequiredService<DatabaseInitializer>();

    try
    {
        await dbInitializer.InitializeAsync();
        Console.WriteLine("Database initialization successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred during database initialization: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Simulate app readiness state
var startTime = DateTime.UtcNow;

// Health probe endpoint
app.MapGet("/healthz", () =>
{
    var uptime = DateTime.UtcNow - startTime;
    return Results.Ok(new
    {
        status = "Healthy",
        uptime = uptime.ToString(@"dd\.hh\:mm\:ss")
    });
});

app.Run();
