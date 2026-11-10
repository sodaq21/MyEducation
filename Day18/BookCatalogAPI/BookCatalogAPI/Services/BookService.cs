using BookCatalogAPI.Models;

namespace BookCatalogAPI.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books =
        [
            new Book(1, "1984", "George Orwell", 1949, DateTime.Now),
            new Book(2, "Преступление и наказание", "Фёдор Достоевский", 1866, DateTime.Now),
            new Book(3, "Мастер и Маргарита", "Михаил Булгаков", 1967, DateTime.Now),
            new Book(4, "Война и мир", "Лев Толстой", 1869, DateTime.Now),
            new Book(5, "Гарри Поттер и философский камень", "Джоан Роулинг", 1997, DateTime.Now),
            new Book(6, "Три товарища", "Эрих Мария Ремарк", 1936, DateTime.Now),
            new Book(7, "Цветы для Элджернона", "Дэниел Киз", 1966, DateTime.Now),
            new Book(8, "Шерлок Холмс: Собака Баскервилей", "Артур Конан Дойл", 1902, DateTime.Now),
            new Book(9, "Маленький принц", "Антуан де Сент-Экзюпери", 1943, DateTime.Now),
            new Book(10, "Чистый код", "Роберт Мартин", 2008, DateTime.Now)
        ];
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
