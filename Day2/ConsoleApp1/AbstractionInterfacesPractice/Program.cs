namespace AbstractionInterfacesPractice
{

    interface IPrintable
    {
        void Print();
    }

    abstract class Shape : IPrintable
    {
        public string Name { get; protected set; }
        public virtual void Print()
        {
            Console.WriteLine($"{GetType().Name} | Area: {GetArea()}");
        }
        public abstract double GetArea();
    }

    class Circle : Shape
    {
        public double Radius { get; }
        public Circle(string name, double r)
        {
            if (r <= 0)
                throw new ArgumentException("Error! Radius must be positive integer.");
            Radius = r;
            Name = name;
        }
        public override double GetArea()
        {
            return Math.Round(Math.PI * Radius * Radius, 2);
        }
    }

    class Rectangle : Shape
    {
        public double Height { get; private set; }
        public double Width { get; private set; }
        public Rectangle(string name, double h, double w)
        {
            if (h <= 0 || w <= 0)
                throw new ArgumentException("Error! Height and width must be positive integer.");
            Name = name;
            Width = w;
            Height = h;
        }
        public override double GetArea()
        {
            return Math.Round(Height * Width, 2);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Creating list of shapes that are different classes
            List<Shape> shapes = new List<Shape>() 
            { 
              new Circle("Small circle", 2), 
              new Rectangle("Rect1", 3, 4), 
              new Rectangle("Rect2", 6, 12) 
            };

            // Calling method Print() for each shape in the list
            foreach (var item in shapes)
            {
                item.Print();
            }

            // Creating shape with invalid parameter and catching the exception
            try
            {
                new Circle("Bad", -15);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            // Testing GetArea() work
            Circle circle = new Circle("Circle", 3.14);
            Console.WriteLine(circle.GetArea());

            Rectangle rect = new Rectangle("Rect", 4, 5);
            Console.WriteLine(rect.GetArea());
        }
    }
}
