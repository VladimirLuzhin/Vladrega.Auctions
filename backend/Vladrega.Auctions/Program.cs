using System.Reflection;
using MediatR;
using Vladrega.Auctions.Application;
using Vladrega.Auctions.Application.Behaviours;
using Vladrega.Auctions.Application.Mediator;
using Vladrega.Auctions.Application.Notifications;
using Vladrega.Auctions.Database.Auctions;
using Vladrega.Auctions.Domain;
using Vladrega.Auctions.SignalR;

var builder = WebApplication.CreateBuilder(args);

var assemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(config => config.AsScoped(), assemblies);
builder.Services.AddSingleton<IRepository<Auction>, InMemoryAuctionsRepository>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<INotificator>(provider => provider.GetRequiredService<NotificationHub>());
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
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(builder =>
{
    builder
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:3000");
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


app.MapControllers();
app.MapHub<NotificationHub>("/notifications");

app.Run();