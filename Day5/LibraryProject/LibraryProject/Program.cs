using System.Runtime.InteropServices.Marshalling;

namespace LibraryProject
{
    // This project was created to consolidate knowledge and skills in OOP
    class LibraryMember
    {
        private List<LibraryItem> _takenItems = new List<LibraryItem>();
        private readonly List<string> _levels = new List<string>{ "Basic", "Advanced", "Premium"};
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
            string current_level = MembershipLevel;
            if (_levels.Contains(level))
            {
                MembershipLevel = level;
                Console.WriteLine($"Status has been changed: {current_level} -> {level}");
                SetProperties(level);
            }
            else
                Console.WriteLine($"There is no {level} level in the list!");

        }

        private void SetProperties(string level)
        {
            switch (level)
            {
                case "Basic":
                    ItemsLimit = 2;
                    break;
                case "Advanced":
                    ItemsLimit = 5;
                    break;
                case "Premium":
                    ItemsLimit = 10;
                    break;
                default:
                    Console.WriteLine($"{level} level not found.");
                    break;
            }
        }

        public void GetLibraryCard()
        {
            Console.WriteLine($"===={Name}'s library card====");
            Console.WriteLine($"Membership level: {MembershipLevel}");
            Console.WriteLine($"You can rent {ItemsLimit - _takenItems.Count} more items. (Upgrade your plan to increase limit))");
            foreach (var item in _takenItems)
            {
                Console.WriteLine($"{item.ToString()}: {item.GetInfo()}");
            }
        }
    }

    abstract record LibraryItem
    {
        public string Title { get; init; }
        public string? Author { get; init; }
        public int? Year { get; init; }
        public int LoanPeriod { get; init; }

        abstract public int GetLoanPeriod();

        public string GetInfo()
        {
            return $"Title: {Title}, author: {Author}, year: {Year}";
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
