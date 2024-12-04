using DataManager.Contract;
using DataManager.Manager;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IEmployeeDataContract, EmployeeDataManager>();

builder.Services.AddControllers();
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

app.UseRouting();

// Add CORS policy
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200") // Replace with your frontend domain
    .AllowAnyMethod()
    .AllowAnyHeader());
app.MapControllers();

app.Run();
