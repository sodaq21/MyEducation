using System.Runtime.InteropServices.Marshalling;

namespace LibraryProject
{
    // This project was created to consolidate knowledge and skills in OOP
    class LibraryMember
    {
        private List<LibraryItem> _takenItems = new List<LibraryItem>();
        private static readonly List<string> _levels = new List<string>{ "Basic", "Advanced", "Premium"};
        public string Name { get; init; }
        private string MembershipLevel { get; set; }
        private int ItemsLimit { get; set; }
        
        public LibraryMember(string name)
        {
            Name = name;
            MembershipLevel = _levels[0];
            SetProperties(MembershipLevel);
        }
        public void SetMemberStatus(string level)
        {
            string currentLevel = MembershipLevel;
            if (_levels.Contains(level))
            {
                if (_levels.IndexOf(MembershipLevel) >= _levels.IndexOf(level))
                {
                    Console.WriteLine($"You can not change level to the same one or lower it!");
                    return;
                }
                MembershipLevel = level;
                Console.WriteLine($"Status has been changed: {currentLevel} -> {level}");
                SetProperties(level);
            }
            else
                Console.WriteLine($"The selected level is not on the list!");
        }

        public void TakeItem(LibraryItem item)
        {
            foreach (var i in _takenItems)
            {
                if (item.Equals(i))
                {
                    Console.WriteLine($"You already have taken the {item}!");
                    return;
                }
            }
            if (_takenItems.Count + 1 <= ItemsLimit)
            {
                _takenItems.Add(item);
                Console.WriteLine($"You have taken the {item.ToString()}!");
            }
            else
            {
                Console.WriteLine($"You reached your limit ({ItemsLimit}). Upgrade your plan to increase limit!");
            }
        }

        public void ReturnItem(LibraryItem item)
        {
            foreach (var i in _takenItems)
            {
                if (item.Equals(i))
                {
                    _takenItems.Remove(item);
                    Console.WriteLine($"You have returned the {item.ToString()}!");
                    return;
                }
                

            }
            Console.WriteLine($"You have not this item!");
        }

        private void SetProperties(string level) => ItemsLimit = level switch
        {
            "Basic" => 2,
            "Advanced" => 5,
            "Premium" => 10,
            _ => ItemsLimit
        };

        public void GetLibraryCard()
        {
            Console.WriteLine();
            Console.WriteLine($"===={Name}'s library card====");
            Console.WriteLine($"Membership level: {MembershipLevel}");
            if (MembershipLevel == "Premium")
                Console.WriteLine($"You can rent {ItemsLimit - _takenItems.Count} more items. (You have reached the maximum level)");
            else
                Console.WriteLine($"You can rent {ItemsLimit - _takenItems.Count} more items. (Upgrade your plan to increase limit)");

            foreach (var item in _takenItems)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine();
        }
    }

    abstract record LibraryItem
    {
        public string Title { get; init; }
        public string? Author { get; init; }
        public int? Year { get; init; }

        public abstract int GetLoanPeriod();

        protected LibraryItem(string title, string? author, int? year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public virtual string GetInfo()
        {
            return $"Title: {Title}, author: {Author}, year: {Year}";
        }
    }

    record class Book : LibraryItem
    {
        public Book(string title, string? author, int? year) : base(title, author, year) { }

        public override int GetLoanPeriod()
        {
            return 14;
        }
    }

    record class DVD : LibraryItem
    {
        public DVD(string title, string? author, int? year) : base(title, author, year){ }

        public override int GetLoanPeriod()
        {
            return 7;
        }
    }

    record class Magazine : LibraryItem
    {
        public Magazine(string title, string? author, int? year) : base(title, author, year) { }

        public override int GetLoanPeriod()
        {
            return 3;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryMember max = new LibraryMember("Max");
            DVD dvd = new DVD("5cent Mix", "5cent", 1995);
            max.GetLibraryCard();
            max.TakeItem(dvd);
            max.TakeItem(dvd);
            max.GetLibraryCard();
            Book book = new Book("Romeo and Juliet", "William Shakespeare", null);
            max.TakeItem(book);
            max.GetLibraryCard();
            max.ReturnItem(dvd);
            max.ReturnItem(dvd);
            max.GetLibraryCard();
            Magazine magazine = new Magazine("Cooking hints", null, null);
            max.TakeItem(magazine);
            max.GetLibraryCard();
            max.TakeItem(dvd);
            max.GetLibraryCard();
            max.SetMemberStatus("Premium");
            max.GetLibraryCard();
            max.TakeItem(dvd);
            max.GetLibraryCard();
            max.SetMemberStatus("Pro");
            max.SetMemberStatus("Basic");
            max.GetLibraryCard();
        }
    }
}
