using System.Net;
using System.Text;

namespace HTTP_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpListener server = new HttpListener();
            HttpClient client = new HttpClient();

            server.Prefixes.Add("http://localhost:2112/");
            server.Start();

            while (true)
            {
                var context = server.GetContext();
                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        Console.WriteLine($"Getting data...");
                        string GetData = "Data from server";
                        var bytes = Encoding.Default.GetBytes(GetData);
                        using (var stream = context.Response.OutputStream)
                        {
                            stream.Write(bytes);
                            stream.Flush();
                        }
                        break;
                    case "POST":
                        Console.WriteLine($"Creating data...");
                        break;
                    case "PUT":
                        Console.WriteLine($"Updating data...");
                        break;
                    case "DELETE":
                        Console.WriteLine($"Deleting data...");
                        break;
                        
                }
                
                context.Response.Close();
            }

            //server.Stop();
            //server.Close();
        }
    }
}
