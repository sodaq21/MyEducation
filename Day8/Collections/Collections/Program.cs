namespace Collections
{

    class User
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> listInt = [1, 2, 3, 4, 5]; // array shell, not suitable for multiple insert/remove operations
            LinkedList<int> l = new LinkedList<int>(); // suitable for multiple insert/remove operations
                                                       // using nodes: each element stores a reference to the previous and the next one
            Dictionary<int, string> dict = new Dictionary<int, string>(); // <key, value>, provides the opportunity for quick search by key

            IEnumerable<User> users = GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID = {user.ID}, name = {user.Name}");
            }
        }

        static IEnumerable<User> GetUsers()
        {
            string[] data = File.ReadAllLines("C:\\Files\\MyEducation\\Day8\\Collections\\Collections\\Data\\data.csv");
            foreach (var line in data)
            {
                string[] user = line.Split(',');

                var newUser = new User
                {
                    ID = int.Parse(user[0]),
                    Name = user[1]
                };

                yield return newUser; // it doesn’t return the entire list of users at once, but only one by one.
            }
        }
    }
}
