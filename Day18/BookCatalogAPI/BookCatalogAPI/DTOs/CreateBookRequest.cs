namespace BookCatalogAPI.DTOs
{
    public record CreateBookRequest(string title, string? author, int year)
    {
    }
}
