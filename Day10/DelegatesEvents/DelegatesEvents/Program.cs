namespace DelegatesEvents
{
    public delegate void ProgressReporter(int percent);

    class DataProcessor
    {
        private readonly ProgressReporter _reporter;

        public DataProcessor(ProgressReporter reporter)
        {
            _reporter = reporter;
        }

        public void ProcessData(int dataSize)
        {
            for (int i = 0; i <= dataSize; i++)
            {
                if (i % 10 != 0)
                    continue;

                _reporter?.Invoke((int)((float)i / dataSize * 100));
            }
        }
    }

    internal class Program
    {
        static void BarReporter(int percent)
        {
            Console.CursorLeft = 0;
            var bars = percent / 10;
            var progressBar = "[" + new string('=', bars) + new string(' ', 10 - bars) + "]";
            Console.Write($"{progressBar} {percent}% Complete");
        }
        static void Main(string[] args)
        {
            DataProcessor dataProcessor = new DataProcessor(BarReporter);
            dataProcessor.ProcessData(100);
            
        }
    }
}
