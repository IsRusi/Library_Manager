using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Infrastructure.Repositories;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Library_Manager.Infrastructure
{
    public static class DataAccessLayerRegistration
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DataStore>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }

    }
}