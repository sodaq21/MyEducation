namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime bd_Alex = new DateTime(2007, 5, 23);
            DateTime bd_Max = new DateTime(2006, 12, 21);
            TimeSpan bd_diff = bd_Alex - bd_Max;
            Console.WriteLine($"{bd_diff.Days}");
        }
    }
}
