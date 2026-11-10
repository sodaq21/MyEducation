using BookCatalogAPI.Models;

namespace BookCatalogAPI.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books = [];
        private int _id = 1;
        private object _lock = new();

        public Task<Book> CreateAsync(string title, string? author, int year)
        {
            lock (_lock)
            {
                var book = new Book(_id++, title, author, year, DateTime.UtcNow);
                _books.Add(book);
                return Task.FromResult<Book>(book);
            }
        }
        public Task<IReadOnlyList<Book>> GetAllAsync(string? author)
        {
            lock (_lock)
            {
                IEnumerable<Book> query = _books;
                if (author is not null)
                    query = query.Where(x => x.Author == author).ToList();

                var result = query.OrderBy(x => x.Id).ToList();
                return Task.FromResult<IReadOnlyList<Book>>(result);
            }
        }

        public Task<Book?> GetByIdAsync(int id)
        {
            lock (_lock)
            {
                var book = _books.FirstOrDefault(x => x.Id == id);
                return Task.FromResult<Book?>(book);
            }
        }

        public Task<bool> DeleteAsync(int id)
        {
            lock (_lock)
            {
                var result = _books.RemoveAll(x => x.Id == id) > 0;
                return Task.FromResult(result);
            }
        }

        public Task<Book?> UpdateAuthorAsync(int id, string? author)
        {
            lock (_lock)
            {
                int index = _books.FindIndex(x => x.Id == id);
                if (index < 0)
                    return Task.FromResult<Book?>(null);

                _books[index] = _books[index] with { Author = author };
                return Task.FromResult<Book?>(_books[index]);
            }
        }

        public Task<Book?> UpdateTitleAsync(int id, string title)
        {
            lock (_lock)
            {
                int index = _books.FindIndex(x => x.Id == id);
                if (index < 0)
                    return Task.FromResult<Book?>(null);

                _books[index] = _books[index] with { Title = title};
                return Task.FromResult<Book?>(_books[index]);
            }
        }

        public Task<Book?> UpdateYearAsync(int id, int year)
        {
            lock (_lock)
            {
                int index = _books.FindIndex(x => x.Id == id);
                if (index < 0)
                    return Task.FromResult<Book?>(null);

                _books[index] = _books[index] with { Year = year };
                return Task.FromResult<Book?>(_books[index]);
            }
        }
    }
}
