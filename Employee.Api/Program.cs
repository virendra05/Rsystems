using DataManager.Contract;
using DataManager.Manager;

var builder = WebApplication.CreateBuilder(args);

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
