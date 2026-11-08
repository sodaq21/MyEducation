using BookCatalogAPI.Models;

namespace BookCatalogAPI.DTOs
{
    public record BookResponse(int Id, string Title, string? Author, int Year)
    {
        public static BookResponse FromBook(Book book)
        {
            return new BookResponse(book.Id, book.Title, book.Author, book.Year);
        }
    }
}
