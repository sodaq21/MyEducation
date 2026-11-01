
using ASPNET_Practice.Services;
using System.Text.Json.Serialization;

namespace ASPNET_Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<ICarService, CarService>();
            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
