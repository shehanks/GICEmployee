var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
