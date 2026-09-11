using System.Runtime.CompilerServices;
using System.Text;

namespace BoxingUnboxing
{
    public interface IPrintable
    {
        void Print();
    }

    struct Point : IPrintable
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            X = x; Y = y;
        }
        public void Print()
        {
            Console.WriteLine($"X: {X}; Y: {Y}");
        }
    }

    internal class Program
    {
        static void PrintCheck(IPrintable printable) // any structs objects passed as parameters will be boxed,
        {                                            // since interfaces store a reference to an object on the heap.        
            printable.Print();
        }
        static void Main(string[] args)
        {
            // boxing
            int a = 5;
            object b = a; // object is reference type so it allocate memory on heap
            // unboxing
            int c = (int)b; // int is value type so it allocate memory on stack

            // decimal d = (decimal)b; // InvalidCastException, because b was boxed to int and it can unbox only to int
            decimal d = (decimal)(int)b; // can be fixed this way

            Point p = new Point(5, 41);
            PrintCheck(p);
        }
    }
}
