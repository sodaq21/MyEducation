using BookCatalogAPI.DTOs;
using BookCatalogAPI.Models;

namespace BookCatalogAPI.Services
{
    public interface IBookService
    {
        Task<Book?> GetByIdAsync(int id);
        Task<IReadOnlyList<Book>> GetAllAsync(string? author);
        Task<Book> CreateAsync(string title, string? author, int year);
        Task<bool> DeleteAsync(int id);
        Task<Book?> UpdateTitleAsync(int id, string title);
        Task<Book?> UpdateAuthorAsync(int id, string? author);
        Task<Book?> UpdateYearAsync(int id, int year);
    }
}
