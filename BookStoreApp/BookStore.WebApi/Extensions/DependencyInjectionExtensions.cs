using BookStore.Application.Common.Behaviors;
using BookStore.Application.Common.Interfaces;
using BookStore.Persistance.Interfaces;
using BookStore.Persistance.Services;
using BookStoreApp.DataAccess.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace BookStore.WebApi.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection.AddScoped<IDbConnection>(options => 
                    new NpgsqlConnection(configuration["BookStoreDb"]));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                        .AddScoped<IAuthorRepository, AuthorRepository>()
                        .AddScoped<IBookRepository, BookRepository>()
                        .AddScoped<IOrderRepository, OrderRepository>()
                        .AddScoped<IDetailRepository, DetailRepository>();
        }

        public static IServiceCollection AddBusiness(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                        .AddTransient<IAuthorService, AuthorService>()
                        .AddTransient<IBookService, BookService>()
                        .AddTransient<IOrderService, OrderService>();
        }

        public static IServiceCollection AddPipelines(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                        .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        }
    }
}
