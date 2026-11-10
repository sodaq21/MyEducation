
using BookCatalogAPI.Services;

namespace BookCatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<IBookService, BookService>();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}
