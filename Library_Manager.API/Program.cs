using Library_Manager.API.ExceptionHandlers;
using Library_Manager.Application;
using Library_Manager.Infrastructure;
using Library_Manager.Infrastructure.Data;
using Library_Manager.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Library_Manager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
            builder.Configuration.AddJsonFile("LibraryConnection.json");
            //builder.Configuration.AddUserSecrets("");

            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddBusinessLogicLayer();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

            builder.Services.AddExceptionHandler<NotFoundDataExceptionHandler>();
            builder.Services.AddExceptionHandler<ExistsExceptionHandler>();         
            builder.Services.AddExceptionHandler<InvalidDataExceptionHandler>();     
            builder.Services.AddExceptionHandler<ExceptionsHandler>();
            builder.Services.AddProblemDetails();


            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();
            
            app.Seed();
            app.UseExceptionHandler(_ => { });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}