using Library_Manager.Application.Interfaces.IService;
using Library_Manager.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Manager.Application
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
