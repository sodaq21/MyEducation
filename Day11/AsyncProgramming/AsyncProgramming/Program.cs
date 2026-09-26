using System.Diagnostics;

namespace AsyncProgramming
{
    internal class Program
    {

        async static Task GetDataFromSourceAsync(int size)
        {
            await Task.Delay(size * 1000);            
        }

        async static Task GetDataAsync1()
        {
            var stopwatch = Stopwatch.StartNew();
            // tasks processing consistently - it takes longer

            // getting data from database
            await GetDataFromSourceAsync(1);
            // getting data from API
            await GetDataFromSourceAsync(2);
            // getting data from file
            await GetDataFromSourceAsync(3);  
            
            stopwatch.Stop();
            Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }

        async static Task GetDataAsync2()
        {
            var stopwatch = Stopwatch.StartNew();
            // tasks processing at the same time - it takes as long as the longest operation will take.
            var DataFromDB = GetDataFromSourceAsync(1);
            var DataFromAPI = GetDataFromSourceAsync(2);
            var DataFromFile = GetDataFromSourceAsync(3);
            await Task.WhenAll(DataFromDB, DataFromAPI, DataFromFile); // wait until all tasks are completed
            stopwatch.Stop();
            Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }
        async static Task Main(string[] args)
        {
            await GetDataAsync1(); // around 6000 ms
            await GetDataAsync2(); // around 3000 ms 
        }
        
    }
}
