using System.Collections.ObjectModel;

namespace GenericTask
{
    public interface IHasId
    {
        int Id { get; }
    }

    class Repository<T> where T : class, IHasId
    {
        private List<T> _values = new List<T>();

        public void Add(T value)
        {
            _values.Add(value);
        }
        public T? GetById(int id)
        {
            return _values.FirstOrDefault(i => i.Id == id);
        }
        public ReadOnlyCollection<T> GetAll() => _values.AsReadOnly();
        public void Remove(int id)
        {
            T? item = _values.FirstOrDefault(i => i.Id == id);
            if (item is null)
            {
                Console.WriteLine("Not found!");
                return;
            }
            _values.Remove(item);
            Console.WriteLine("Success!");
        }
    }

    class User : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    class Book : IHasId
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Book(int id, string t)
        {
            Id = id;
            Title = t;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository<User> userRepository = new Repository<User>();
            User user1 = new User(1, "Max");
            User user2 = new User(2, "Alex");
            Book book1 = new Book(1, "Harry Potter");
            Book book2 = new Book(2, "Kolobok");
            userRepository.Add(user1);
            userRepository.Add(user2);
            
            User? newUser = userRepository.GetById(2);
            if (newUser != null)
                Console.WriteLine(newUser.Name);
            else
                Console.WriteLine("User not found!");

            ReadOnlyCollection<User> users = userRepository.GetAll();
            foreach (var item in users)
            {
                Console.WriteLine(item.Name);
            }
            userRepository.Remove(1);
            users = userRepository.GetAll();

            Repository<Book> booksRepository = new Repository<Book>();
            booksRepository.Add(book1);
            booksRepository.Add(book2);
            ReadOnlyCollection<Book> books = booksRepository.GetAll();
            foreach (var item in books)
            {
                Console.WriteLine(item.Title);
            }
        }
    }
}
