using BookStore.Application.Books.Commands.CreateBook;
using BookStore.Application.Books.Commands.UpdateBook;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Messaging;
using BookStore.Mongo.Infrastructure;
using BookStore.SyncronizationUnit.Factories;
using BookStore.WebApi.Configurations;
using BookStore.WebApi.Extensions;
using BookStoreApp.DataAccess.Messaging;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMediatR(typeof(IService<,>).Assembly);

builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("MessageBrokerSettings"));
builder.Services.AddSingleton(provider => provider.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton(provider => provider.GetRequiredService<IOptions<MongoSettings>>().Value);

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    options.AddProfile(new AssemblyMappingProfile(typeof(IService<,>).Assembly));
});

builder.Services.AddRepositories();
builder.Services.AddBusiness();
builder.Services.AddPipelines();
builder.Services.AddValidatorsFromAssembly(typeof(IService<,>).Assembly, includeInternalTypes: true);
builder.Services.AddMassTransit(options =>
{
    options.AddBookEventConsumers();
    options.AddAuthorEventConsumers();
    options.AddDetailEventConsumers();
    options.AddOrderEventConsumers();

    options.SetKebabCaseEndpointNameFormatter();
    options.UsingRabbitMq((context, configurator) =>
    {
        var settings = context.GetRequiredService<MessageBrokerSettings>();
        configurator.Host(new Uri(settings.Host), host =>
        {
            host.Username(settings.Username);
            host.Password(settings.Password);
        });
    });
});

builder.Services.AddTransient<ISyncronizationUnitFactory, SyncronizationUnitFactory>();
builder.Services.AddTransient<IEventBus, EventBus>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
