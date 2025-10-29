using Library_Manager.Infrastructure.Data;
using Library_Manager.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library_Manager.API
{
    public static class DataSeederRegistration
    {
        public static void Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try {
               
                var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();

                var authorSeeder = scope.ServiceProvider.GetRequiredService<AuthorDbInitializer>();
                AuthorDbInitializer.Seed(dbContext);

                var bookSeeder = scope.ServiceProvider.GetRequiredService<BookDbInitializer>();
                BookDbInitializer.Seed(dbContext);
            }catch(Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Произошла ошибка при начальном заполнении базы данных.");
            }
            }

    }
    }
