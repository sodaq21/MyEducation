using System.ComponentModel;

namespace LinqPractice
{
    class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * дан список объектов Order { int Id, string CustomerName, decimal Total, DateTime Date, string Status }
             * Напиши LINQ-запросы: 
             * (1) все заказы за последние 30 дней со статусом “Completed”, отсортированные по сумме по убыванию; 
             * (2) сгруппировать заказы по клиенту и вывести сумму заказов на клиента; 
             * (3) найти клиента с самым большим суммарным заказом; 
             * (4) проверить, есть ли хотя бы один заказ дороже 1000. 
            */
            var orders = new List<Order>
            {
                new Order { Id = 1, CustomerName = "Alex Johnson", Total = 125.50m, Date = new DateTime(2026, 1, 5), Status = "Completed" },
                new Order { Id = 2, CustomerName = "Maria Smith", Total = 89.99m, Date = new DateTime(2026, 1, 8), Status = "Pending" },
                new Order { Id = 3, CustomerName = "John Brown", Total = 245.00m, Date = new DateTime(2026, 1, 12), Status = "Completed" },
                new Order { Id = 4, CustomerName = "Daniel White", Total = 59.50m, Date = new DateTime(2026, 1, 15), Status = "Cancelled" },
                new Order { Id = 5, CustomerName = "Michael Wilson", Total = 310.75m, Date = new DateTime(2026, 1, 19), Status = "Shipped" },
                new Order { Id = 6, CustomerName = "Sarah Miller", Total = 45.00m, Date = new DateTime(2026, 1, 22), Status = "Completed" },
                new Order { Id = 7, CustomerName = "Daniel White", Total = 178.25m, Date = new DateTime(2026, 1, 25), Status = "Pending" },
                new Order { Id = 8, CustomerName = "Anna Anderson", Total = 520.00m, Date = new DateTime(2026, 1, 28), Status = "Completed" },
                new Order { Id = 9, CustomerName = "Maria Smith", Total = 74.99m, Date = new DateTime(2026, 9, 2), Status = "Shipped" },
                new Order { Id = 10, CustomerName = "Maria Smith", Total = 199.90m, Date = new DateTime(2026, 9, 5), Status = "Completed" },

                new Order { Id = 11, CustomerName = "Daniel White", Total = 350.40m, Date = new DateTime(2026, 2, 8), Status = "Pending" },
                new Order { Id = 12, CustomerName = "Sophia Harris", Total = 129.99m, Date = new DateTime(2026, 2, 11), Status = "Completed" },
                new Order { Id = 13, CustomerName = "James Martin", Total = 67.50m, Date = new DateTime(2026, 2, 14), Status = "Cancelled" },
                new Order { Id = 14, CustomerName = "Olivia Thompson", Total = 415.00m, Date = new DateTime(2026, 2, 17), Status = "Shipped" },
                new Order { Id = 15, CustomerName = "William Garcia", Total = 92.30m, Date = new DateTime(2026, 8, 20), Status = "Completed" },
                new Order { Id = 16, CustomerName = "Emma Martinez", Total = 275.75m, Date = new DateTime(2026, 8, 23), Status = "Pending" },
                new Order { Id = 17, CustomerName = "Christopher Robinson", Total = 49.99m, Date = new DateTime(2026, 2, 26), Status = "Completed" },
                new Order { Id = 18, CustomerName = "Isabella Clark", Total = 630.00m, Date = new DateTime(2026, 3, 1), Status = "Shipped" },
                new Order { Id = 19, CustomerName = "Daniel White", Total = 155.25m, Date = new DateTime(2026, 3, 4), Status = "Completed" },
                new Order { Id = 20, CustomerName = "Mia Lewis", Total = 88.80m, Date = new DateTime(2026, 3, 7), Status = "Cancelled" },

                new Order { Id = 21, CustomerName = "Andrew Lee", Total = 340.00m, Date = new DateTime(2026, 3, 10), Status = "Pending" },
                new Order { Id = 22, CustomerName = "Charlotte Walker", Total = 215.49m, Date = new DateTime(2026, 3, 13), Status = "Completed" },
                new Order { Id = 23, CustomerName = "Ryan King", Total = 59.99m, Date = new DateTime(2026, 8, 16), Status = "Shipped" },
                new Order { Id = 24, CustomerName = "Ryan King", Total = 480.50m, Date = new DateTime(2026, 9, 19), Status = "Completed" },
                new Order { Id = 25, CustomerName = "Joshua Young", Total = 135.75m, Date = new DateTime(2026, 9, 22), Status = "Pending" },
                new Order { Id = 26, CustomerName = "Harper Hernandez", Total = 720.00m, Date = new DateTime(2026, 3, 25), Status = "Completed" },
                new Order { Id = 27, CustomerName = "Ryan King", Total = 105.20m, Date = new DateTime(2026, 3, 28), Status = "Shipped" },
                new Order { Id = 28, CustomerName = "Maria Smith", Total = 295.99m, Date = new DateTime(2026, 4, 2), Status = "Completed" },
                new Order { Id = 29, CustomerName = "Nicholas Lopez", Total = 64.50m, Date = new DateTime(2026, 9, 5), Status = "Cancelled" },
                new Order { Id = 30, CustomerName = "Abigail Hill", Total = 390.00m, Date = new DateTime(2026, 4, 8), Status = "Pending" },

                new Order { Id = 31, CustomerName = "Ethan Scott", Total = 175.80m, Date = new DateTime(2026, 4, 11), Status = "Completed" },
                new Order { Id = 32, CustomerName = "Ella Green", Total = 525.25m, Date = new DateTime(2026, 4, 14), Status = "Shipped" },
                new Order { Id = 33, CustomerName = "Alexander Adams", Total = 99.99m, Date = new DateTime(2026, 9, 17), Status = "Completed" },
                new Order { Id = 34, CustomerName = "Grace Baker", Total = 250.00m, Date = new DateTime(2026, 9, 20), Status = "Pending" },
                new Order { Id = 35, CustomerName = "Benjamin Nelson", Total = 410.60m, Date = new DateTime(2026, 4, 23), Status = "Completed" },
                new Order { Id = 36, CustomerName = "Chloe Carter", Total = 72.45m, Date = new DateTime(2026, 4, 26), Status = "Shipped" },
                new Order { Id = 37, CustomerName = "Lucas Mitchell", Total = 185.00m, Date = new DateTime(2026, 8, 29), Status = "Completed" },
                new Order { Id = 38, CustomerName = "Lily Perez", Total = 345.90m, Date = new DateTime(2026, 5, 2), Status = "Pending" },
                new Order { Id = 39, CustomerName = "Henry Roberts", Total = 59.00m, Date = new DateTime(2026, 9, 5), Status = "Cancelled" },
                new Order { Id = 40, CustomerName = "Ryan King", Total = 275.40m, Date = new DateTime(2026, 5, 8), Status = "Completed" }
            };

            ////task 1
            //var task1 = orders
            //    .Where(order => order.Date > DateTime.Today.AddDays(-30))
            //    .Where(order => order.Status == "Completed")
            //    .OrderByDescending(order => order.Total);
            //foreach (var item in task1)
            //{
            //    Console.WriteLine($"ID: {item.Id}, CustomerName: {item.CustomerName}, Total: {item.Total}, Date: {item.Date.ToString("dd/MM/yyyy HH:mm")}, Status: {item.Status}");
            //}

            ////task 2
            //var task2 = orders
            //    .GroupBy(order => order.CustomerName)
            //    .Select(group => new
            //    {
            //        Name = group.Key,
            //        OrdersTotal = group.Sum(order => order.Total)
            //    });
            //foreach (var customer in task2)
            //{
            //    Console.WriteLine(customer);
            //}

            ////task 3
            //var task3 = orders
            //    .GroupBy(order => order.CustomerName)
            //    .Select(group => new
            //    {
            //        Name = group.Key,
            //        OrdersTotal = group.Sum(order => order.Total)
            //    })
            //    .OrderByDescending(order => order.OrdersTotal)
            //    .First();
            //Console.WriteLine($"CustomerName: {task3.Name}, Total: {task3.OrdersTotal}");

            ////task 4
            //var task4 = orders.Any(order => order.Total > 1000);
            //Console.WriteLine(task4);

            // task 5 Построй для каждого клиента отчёт: количество заказов, сумма всех заказов, средний чек.
            // Оставь в результате только тех клиентов, у которых больше одного заказа, отсортируй по сумме по убыванию.
            var task5 = orders
                .GroupBy(order => order.CustomerName)
                .Select(group => new
                {
                    Name = group.Key,
                    OrdersCount = group.Count(),
                    OrdersTotal = group.Sum(order => order.Total),
                    AverageTotal = group.Average(order => order.Total)
                })
                .Where(customer => customer.OrdersCount > 1)
                .OrderByDescending(order => order.OrdersTotal);
            foreach (var customer in task5)
            {
                Console.WriteLine(customer);
            }

            // task 6 Посчитай количество заказов по месяцу и статусу одновременно —
            // то есть результат вида "Январь 2026, Completed → 3 заказа", "Январь 2026, Pending → 2 заказа"
            // и так далее для всех месяцев/статусов, где есть хотя бы один заказ.

            var task6 = orders
                .GroupBy(order => new
                {
                    Year = order.Date.Year,
                    Month = order.Date.Month,
                    Status = order.Status
                })
                .Select(group => new
                {
                    Date = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yyyy"),
                    Status = group.Key.Status,
                    OrdersCount = group.Count()
                })
                .Where(group => group.OrdersCount >= 1);
            foreach (var order in task6)
            {
                Console.WriteLine($"{order.Date}, {order.Status} => {order.OrdersCount} заказа(ов).");
            }

            // task7 Найди все заказы со статусом "Completed", сделанные VIP-клиентами,
            // и выведи их вместе с пометкой "VIP" рядом с именем.
            var vipCustomers = new List<string> { "Maria Smith", "Ryan King", "Daniel White" };
            var task7 = orders
                .Join(vipCustomers,
                order => order.CustomerName,
                name => name,
                (order, name)
                => new
                {
                    Status = order.Status,
                    Name = name,
                    ID = order.Id
                })
                .Where(order => order.Status == "Completed");
            
            var task7_2 = orders
                .Where(order => vipCustomers.Contains(order.CustomerName))
                .Where(order => order.Status == "Completed");
            foreach (var order in task7)
            {
                Console.WriteLine($"VIP {order.Name}, Order Id: {order.ID}, Status: {order.Status}");
            }
            foreach (var order in task7_2)
            {
                Console.WriteLine($"VIP {order.CustomerName}, Order Id: {order.Id}, Status: {order.Status}");
            }

            // task8 Отсортируй все заказы по дате (сначала новые),
            // затем выведи вторую страницу результатов при размере страницы 10 (то есть заказы с 11-го по 20-й по этой сортировке).
            var task8 = orders
                .OrderByDescending(order => order.Date)
                .Skip(10)
                .Take(10);
            if (task8 is null)
                return;
            foreach (var order in task8)
            {
                Console.WriteLine($"ID: {order.Id}, Date: {order.Date}");
            }

        }
    }
}
