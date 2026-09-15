namespace Collections
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> listInt = [1, 2, 3, 4, 5]; // array shell, not suitable for multiple insert/remove operations
            LinkedList<int> l = new LinkedList<int>(); // suitable for multiple insert/remove operations
                                                       // using nodes: each element stores a reference to the previous and the next one
            Dictionary<int, string> dict = new Dictionary<int, string>(); // <key, value>, provides the opportunity for quick search by key
        }
    }
}
