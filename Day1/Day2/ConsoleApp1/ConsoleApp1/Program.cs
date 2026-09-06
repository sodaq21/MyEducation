namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Value types - structs and enums: int, bool, DateTime, etc.
            // They contain the actual value. Where they are stored depends on the context.

            // Reference types - classes, arrays, strings, etc.
            // Variables contain a reference to an object. The object is typically allocated on the heap.

            // Nullable value types <type>?
            int? x = 5;
            Console.WriteLine(x.HasValue); // true
            Console.WriteLine(x.Value); // 5
            Console.WriteLine(x.GetValueOrDefault()); // 5

            int? y = null;
            Console.WriteLine(y.HasValue); // false
            Console.WriteLine(y.Value); // InvalidOperationException
            Console.WriteLine(y.GetValueOrDefault()); // 0
            Console.WriteLine(y.GetValueOrDefault(52)); // 52 
        }
    }
}
