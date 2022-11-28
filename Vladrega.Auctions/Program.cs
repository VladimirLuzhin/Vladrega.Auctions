using System.Reflection;
using MediatR;
using Vladrega.Auctions.Application;
using Vladrega.Auctions.Application.Behaviours;
using Vladrega.Auctions.Application.Mediator;
using Vladrega.Auctions.Database.Auctions;
using Vladrega.Auctions.Database.Bets;
using Vladrega.Auctions.Database.Lots;
using Vladrega.Auctions.Domain;

var builder = WebApplication.CreateBuilder(args);

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.AsScoped(), assemblies);
builder.Services.AddSingleton<IRepository<Auction>, InMemoryAuctionsRepository>();
builder.Services.AddSingleton<IRepository<Lot>, InMemoryLotsRepository>();
builder.Services.AddSingleton<IRepository<Bet>, InMemoryBetsRepository>();
builder.Services.AddSingleton<UnitOfWork>();

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