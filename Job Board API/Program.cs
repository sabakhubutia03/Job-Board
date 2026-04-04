using Application.Interface;
using Infrastructure.Service;
using Job_Board_API.Data;
using Job_Board_API.JobServices;
using Job_Board_API.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Job_BoardDb")));

builder.Services.AddOpenApi();

builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IJobService,JobService>();
builder.Services.AddScoped<ICompanyService,CompanyService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();