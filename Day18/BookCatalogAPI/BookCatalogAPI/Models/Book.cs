namespace BookCatalogAPI.Models
{
    public record Book(int Id, string Title, string? Author, int Year, DateTime CreatedAt)
    {
        
    }
}
