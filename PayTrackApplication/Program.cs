global using PayTrackApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PayTrackApplication.Application.Services.PayTrackServices;
using PayTrackApplication.Infrastructure.Database;
using PayTrackApplication.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSqlServer<PayTrackApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
string conString = builder.Configuration.GetSection("ConnectionString:DefaultConnection").Value ;
builder.Services.AddSqlServer<PayTrackApplicationDbContext>(conString);
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeePolicyRenewalValidationRepository, EmployeePolicyRenewalValidationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INpiPolicyRepository, NpiPolicyRepository>();
builder.Services.AddScoped<INpiPolicyRuleRepository, NpiPolicyRuleRepository>();


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

app.Run();
