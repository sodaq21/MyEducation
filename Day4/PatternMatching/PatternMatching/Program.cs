using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace PatternMatching
{
    public record Package(string Destination, double WeightKg, decimal DeclaredValue, bool IsExpress);

    internal class Program
    {
        private static readonly HashSet<String> DomesticCities = new HashSet<string> { "Warsaw", "Krakow", "Gdansk" };
        public static string ClassifyPackage(Package package) => package switch
        {
            { IsExpress: true, DeclaredValue: > 1000m } => "VIP-Express",
            { WeightKg: > 30 } => "Freight",
            { Destination: "Warsaw", WeightKg: <= 1 } => "Local-Light",
            _ => "Standart"
        };

        public static bool NeedsCustomCheck(Package package)
        {
            
            if (package is (var destination, _, var value, _) && !DomesticCities.Contains(destination)
                                                              && value > 500)
                return true;
            return false;
        }

        public static decimal CalculateShippingCost(Package package)
        {
            return package switch
            {
                { WeightKg: > 20, IsExpress: true } => 250m,
                var p when p.WeightKg > 0 && (p.DeclaredValue / (decimal)p.WeightKg > 100m) => 350m,
                { WeightKg: >= 100 } => 225m,
                { WeightKg: >= 50 } => 210m,
                _ => 175m
            };

        }

        public static string Describe(Package package) => package switch
        {
            { IsExpress: true, DeclaredValue: > 1000m } => $"This package is VIP-Express. It will be delivered to {package.Destination} for a 1-2 days.",
            (var destination, _, var value, _) when !DomesticCities.Contains(destination) && value > 500 => $"The package requires customs inspection! Value: {value}, destination: {destination}.",
            _ => "Standart package."
        };
        
        static void Main(string[] args)
        {
            var packages = new List<Package>
            {
                new Package("Warsaw", 120, 1000m, true),
                new Package("Moscow", 31, 505m, false),
                new Package("Berlin", 15, 1001m, true),
                new Package("Krakow", 12, 250m, false)
            };

            foreach (var p in packages)
            {
                Console.WriteLine(p);
                Console.WriteLine(Program.ClassifyPackage(p));
                Console.WriteLine(Program.NeedsCustomCheck(p));
                Console.WriteLine(Program.CalculateShippingCost(p));
                Console.WriteLine(Program.Describe(p));
                Console.WriteLine();
            }
        }
    }
}
