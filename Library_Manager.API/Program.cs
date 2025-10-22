using Library_Manager.Application;
using Library_Manager.Infrastructure;
namespace Library_Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            Context context= new();
            context.Seed();
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddBusinessLogicLayer();

            builder.Services.AddProblemDetails();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var app = builder.Build();

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
