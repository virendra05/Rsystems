using Serilog;
using Serilog.Formatting.Json;
using DataManager.Contract;
using DataManager.Manager;
using BusinessManager.Contract;
using BusinessManager.Manager;

var builder = WebApplication.CreateBuilder(args);
var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.File(
        formatter: new JsonFormatter(),
        path: Path.Combine(logDirectory, "Log{Date}.json"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: null,
        shared: true)
    .CreateLogger();

builder.Services.AddSingleton<ILoggingService, SerilogLoggingService>();
builder.Services.AddScoped<IEmployeeBusinessContract, EmployeeBusinessManager>();
builder.Services.AddScoped<IEmployeeDataContract, EmployeeDataManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();

app.Run();
