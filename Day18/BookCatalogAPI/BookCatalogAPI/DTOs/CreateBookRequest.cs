namespace BookCatalogAPI.DTOs
{
    public record CreateBookRequest(string Title, string? Author, int Year)
    {
    }
}
