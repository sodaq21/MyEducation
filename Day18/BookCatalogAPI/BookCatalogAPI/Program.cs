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
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSingleton<IBookService, BookService>();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateBookRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateTitleRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuthorRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateYearRequestValidator>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
