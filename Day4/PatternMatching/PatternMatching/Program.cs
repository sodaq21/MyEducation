using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace PatternMatching
{
    public record Package(string Destination, double WeightKg, decimal DeclaredValue, bool IsExpress);

    
    

    internal class Program
    {
        public string ClassifyPackage(Package package) => package switch
        {
            { IsExpress: true, DeclaredValue: > 1000 } => "VIP Express",
            { WeightKg: > 30 } => "Freight",
            { Destination: "Warsaw", WeightKg: <= 1 } => "Local-light",
            { } => "Standart"
        };

        //public bool NeedsCustomCheck(Package package)
        //{
        //    if (package is (var destination, _, var value, _) && )
        //}

        public decimal CalculateShippingCost(Package package)
        {
            switch (package)
            {
                case Package p when p.IsExpress && p.WeightKg > 20:
                    return 250.0m;
                case Package p when (p.DeclaredValue / (decimal)p.WeightKg) > 100:
                    return 350.0m;
                default:
                    return 150.0m;
            }
        }

        static void Main(string[] args)
        {
            
        }
    }
}
