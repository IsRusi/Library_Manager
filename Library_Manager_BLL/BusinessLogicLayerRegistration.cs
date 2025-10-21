using Library_Manager_BLL.Services;
using Library_Manager_DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Manager_BLL
{
    public static class BusinessLogicLayerRegistration
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();

            return services;
        }
    }
}
