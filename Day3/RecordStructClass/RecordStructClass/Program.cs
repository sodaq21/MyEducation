namespace RecordStructClass
{

    readonly struct Point2D
    {
        public int X { get; init; }
        public int Y { get; init; }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point2D Move(int dx, int dy)
        {
            return new Point2D(X + dx, Y + dy);
        }
    }

    class Counter
    {
        public int Value { get; private set; }

        public Counter(int x)
        {
            Value = x;
        }
        public void Increment()
        {
            Value++;
        }
    }

    record PersonSnapshot(string Name, int Age);

    class PersonClass
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public PersonClass(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Point2D(5, 1);
            var p2 = p1; // p2 is an independent copy
            p2 = p2.Move(2, 5); 
            // p1 values didn't change, p2 values did
            Console.WriteLine($"p1: ({p1.X}; {p1.Y})"); // p1: (5; 1)
            Console.WriteLine($"p2: ({p2.X}; {p2.Y})"); // p2: (7; 6)

            var c1 = new Counter(1);
            var c2 = c1; // c2 copies c1 reference, c1 and c2 refers to the same value
            c2.Increment();
            Console.WriteLine($"c1: {c1.Value}"); // c1: 2
            Console.WriteLine($"c2: {c2.Value}"); // c2: 2

            var pers1 = new PersonSnapshot("Kostya", 21);
            var pers2 = new PersonSnapshot("Kostya", 21);
            Console.WriteLine(pers1 == pers2); // true, compares by values
            Console.WriteLine(ReferenceEquals(pers1, pers2)); // false, compares by references
            var olderPers = pers1 with { Age = 50 };
            Console.WriteLine(pers1); // records has overriden ToString()
            Console.WriteLine(olderPers);

            var persc1 = new PersonClass("Alex", 15);
            var persc2 = new PersonClass("Alex", 15);
            Console.WriteLine(persc1 == persc2); // false, compares by references
        }
    }
}
