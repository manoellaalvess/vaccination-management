using MediatR;
using Microsoft.OpenApi.Models;
using VaccinationManagement.CrossCutting.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a Controllers (MVC)
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("VaccinationManagement.Application")));



// OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mediator
builder.Services.AddMediator();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configura o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// serve /wwwroot e torna index.html a página padrão
app.UseDefaultFiles();
app.UseStaticFiles();

// Mapear Controllers
app.MapControllers();

app.Run();