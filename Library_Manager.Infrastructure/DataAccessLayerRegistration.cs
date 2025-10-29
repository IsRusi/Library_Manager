using Library_Manager.Application.Interfaces.IRepository;
using Library_Manager.Infrastructure.Data;
using Library_Manager.Infrastructure.Profiles;
using Library_Manager.Infrastructure.Repositories;
using Library_Manager.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Manager.Infrastructure
{
    public static class DataAccessLayerRegistration
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<DataStore>();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();
                cfg.AddProfile<BookProfile>();
            });
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<LibraryContext>(optionsAction => optionsAction.UseSqlServer(connectionString));

            services.AddScoped<DataStore>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<AuthorDbInitializer>();
            services.AddScoped<BookDbInitializer>();


            return services;
        }
    }
}