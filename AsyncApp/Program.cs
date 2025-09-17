using System;
using System.Threading.Tasks;

namespace LoadBalancerApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Program started.");
    
            await HandleMultipleRequestsAsync();

            Console.WriteLine("Program continues after API requests.");
        }

        // Async method simulating an API call
        static async Task<string> FetchDataAsync()
        {
            await Task.Delay(2000); // Simulate a 2-second API call
            return $"API call complete at {DateTime.Now:T}";
        }

        static async Task HandleMultipleRequestsAsync()
        {
            Console.WriteLine("Starting multiple API requests...");

            var tasks = new List<Task<string>>
            {
                FetchDataAsync(),
                FetchDataAsync(),
                FetchDataAsync()
            };

            string[] results = await Task.WhenAll(tasks);

            Console.WriteLine("All API requests completed:");
            foreach (var result in results)
            {
                Console.WriteLine($"  - {result}");
            }
        }

    }
}
