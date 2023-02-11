using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappings;
using BookStore.WebApi.Extensions;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMediatR(typeof(IService<,>).Assembly);

builder.Services.AddAutoMapper(options =>
{
    options.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    options.AddProfile(new AssemblyMappingProfile(typeof(IService<,>).Assembly));
});

builder.Services.AddRepositories();
builder.Services.AddBusiness();
builder.Services.AddPipelines();
builder.Services.AddValidatorsFromAssembly(typeof(IService<,>).Assembly, includeInternalTypes: true);

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
