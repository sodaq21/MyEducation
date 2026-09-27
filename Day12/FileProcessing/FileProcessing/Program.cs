namespace FileProcessing
{
    class Order
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public decimal Total { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get; set; }
    }

    internal class Program
    {
        async static Task<List<Order>> GetOrdersAsync(string path)
        {
            List<Order> orders = new List<Order>();
            try
            {
                if (File.Exists(path))
                {
                    using (var reader = new StreamReader(path))
                    {
                        string? line;
                        bool firstLine = true;
                        while ((line = await reader.ReadLineAsync()) != null)
                        {
                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            if (firstLine)
                            {
                                firstLine = false;
                                continue;
                            }

                            string[] props = line.Split(',');
                            orders.Add(new Order
                            {
                                Id = int.Parse(props[0]),
                                CustomerName = props[1],
                                Total = decimal.Parse(props[2], System.Globalization.CultureInfo.InvariantCulture),
                                Date = DateTime.Parse(props[3]),
                                Status = props[4]
                            });
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.FileName}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            return orders;
        }

        async static Task<bool> WriteReportByOrderStatusAsync(List<Order> orders, string directory, string fileName)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string fullPath = Path.Combine(directory, fileName);

            if (File.Exists(fullPath))
            {
                Console.WriteLine("Error: file in the specified path already exists!");
                return false;
            }

            var ordersByStatus = orders
                .GroupBy(o => o.Status)
                .Select(group => new
                {
                    Status = group.Key,
                    Sum = group.Sum(o => o.Total),
                    Count = group.Count()
                });
            var ordersByStatusList = ordersByStatus.ToList();

            try
            {
                using (var writer = new StreamWriter(fullPath))
                {
                    foreach (var status in ordersByStatusList)
                    {
                        string statusInfo = $"Status: {status.Status},\tcount: {status.Count},\ttotal: {status.Sum};";
                        await writer.WriteLineAsync(statusInfo);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return true;
        }

        async static Task Main(string[] args)
        {
            string path = @"C:\\Files\\MyEducation\\Day12\\FileProcessing\\FileProcessing\\Data\\orders.csv";
            var orders = await GetOrdersAsync(path);
            if (await WriteReportByOrderStatusAsync(orders, @"E:\\asd", "orders.txt"))
            {
                Console.WriteLine($"Data has been written sucessfully!");
            }
            else
            {
                Console.WriteLine($"Something went wrong!");
            }
        }
    }
}
