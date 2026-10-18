using Microsoft.Extensions.DependencyInjection;

namespace DI_Practice
{
    class A { }
    class B { }
    class C { }
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<A>();
            services.AddScoped<B>();
            services.AddSingleton<C>();

            var provider = services.BuildServiceProvider();
            B b1;
            C c1;
            using (var scope1 = provider.CreateScope())
            {
                var sp = scope1.ServiceProvider;
                b1 = sp.GetRequiredService<B>();
                c1 = sp.GetRequiredService<C>();
                var b2 = sp.GetRequiredService<B>();
                var a1 = sp.GetRequiredService<A>();
                var a2 = sp.GetRequiredService<A>();
                Console.WriteLine(ReferenceEquals(b1, b2)); // false
                Console.WriteLine(ReferenceEquals(a1, a2)); // true
            }
            using (var scope2 = provider.CreateScope())
            {
                var sp = scope2.ServiceProvider;
                var b3 = sp.GetRequiredService<B>();
                var c3 = sp.GetRequiredService<C>();
                Console.WriteLine(ReferenceEquals(b1, b3)); // false
                Console.WriteLine(ReferenceEquals(c1, c3)); // true
            }
        }
    }
}
