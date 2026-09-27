namespace ConsoleApp1
{
    internal class Program
    {
        async static Task F()
        {
            await Task.Delay(5000);
            Console.WriteLine("Func");
        }

        async static Task Main(string[] args)
        {
            await Task.Delay(1500);
            var task = F(); // запустили, НЕ ждём
            Console.WriteLine("Main"); // это выполнится немедленно
            await task; // 
        }
    }
}
