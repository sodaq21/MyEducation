
using BookCatalogAPI.Services;
using BookCatalogAPI.Validators;
using FluentValidation;

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
            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateTitleRequestValidator>();

            var app = builder.Build();
            
            app.MapControllers();

            app.Run();
        }
    }
}
