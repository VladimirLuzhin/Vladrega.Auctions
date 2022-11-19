using System.Reflection;
using MediatR;
using Vladrega.Auctions.Application.Auctions.CreateAuction;
using Vladrega.Auctions.Application.Behaviours;
using Vladrega.Auctions.Application.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.AsScoped(), assemblies);

var openGenericType = typeof(IValidator<>);
var types = assemblies
    .SelectMany(a => a
        .GetTypes()
        .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition));

foreach (var type in types)
{
    var validatorInterface = type
        .GetInterfaces()
        .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == openGenericType);
    
    if (validatorInterface != null)
        builder.Services.AddSingleton(validatorInterface, type);
}

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

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