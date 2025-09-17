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
            await ProcessBackgroundTasksAsync();

            Console.WriteLine("\nProgram finished.");
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
        
        static async Task ProcessBackgroundTasksAsync()
        {
            Console.WriteLine("Starting background task processing...");

            var backgroundTasks = new List<Task>();

            // Simulate 5 background jobs
            for (int i = 1; i <= 5; i++)
            {
                int jobId = i;
                var task = Task.Run(async () =>
                {
                    Console.WriteLine($"[Job {jobId}] Starting...");
                    await Task.Delay(1000 + jobId * 500); // Simulate variable job times
                    Console.WriteLine($"[Job {jobId}] Completed.");
                });

                backgroundTasks.Add(task);
            }

            // Wait for all background jobs to finish
            await Task.WhenAll(backgroundTasks);

            Console.WriteLine("All background tasks completed.");
        }

    }
}
