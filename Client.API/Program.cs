using Client.API.Filters;
using Client.Application.DTOs.InputModels;
using Client.Application.Interfaces;
using Client.Application.Services;
using Client.Application.Validators;
using Client.Domain.Interfaces.Repositories;
using Client.Domain.Interfaces.Services;
using Client.Domain.Services;
using Client.Infra.Data.Context;
using Client.Infra.Data.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddDbContext<ClientDbContext>(opt => opt.UseInMemoryDatabase("ClientDB"));

builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)))
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<AddCustomerInputModelValidator>());

var sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClientDbContext>(opt =>
            opt.UseSqlServer(sqlConnection));

builder.Services.AddScoped<ICustomerServiceApp, CustomerServiceApp>();
builder.Services.AddScoped<ICustomerServiceDomain, CustomerServiceDomain>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

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

app.Run();
