using Library_Manager_DAL.Interfaces;
using Library_Manager_DAL.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Manager_DAL
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
