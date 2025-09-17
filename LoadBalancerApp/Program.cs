using System;
using System.Collections.Generic;
using System.Linq;

namespace LoadBalancerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Simulated client IPs
            var clientRequests = new List<string>
            {
                "192.168.0.1",
                "192.168.0.2",
                "10.0.0.5",
                "172.16.0.7",
                "192.168.0.1",
                "10.0.0.9",
                "192.168.0.3",
                "172.16.0.8",
                "192.168.0.4"
            };

            Console.WriteLine("=== ROUND-ROBIN ===");
            var rrResults = RunSimulation(clientRequests, DistributionType.RoundRobin);

            Console.WriteLine("\n=== IP HASHING ===");
            var ipHashResults = RunSimulation(clientRequests, DistributionType.IPHash);

            Console.WriteLine("\n=== LEAST CONNECTIONS ===");
            var lcResults = RunSimulation(clientRequests, DistributionType.LeastConnections);

            Console.WriteLine("\n\n=== COMPARISON RESULTS ===");

            PrintDistributionSummary("Round-Robin", rrResults);
            PrintDistributionSummary("IP Hashing", ipHashResults);
            PrintDistributionSummary("Least Connections", lcResults);

            Console.WriteLine("\n=== ANALYSIS ===");
            Console.WriteLine("Round-Robin: Best for evenly spreading unknown traffic.");
            Console.WriteLine("IP Hashing: Good for session persistence (same IP -> same server).");
            Console.WriteLine("Least Connections: Best when request load is uneven and long-running.");
        }

        enum DistributionType { RoundRobin, IPHash, LeastConnections }

        static List<Server> RunSimulation(List<string> requests, DistributionType type)
        {
            var loadBalancer = new LoadBalancer();
            var servers = new List<Server>
            {
                new Server("Server-1"),
                new Server("Server-2"),
                new Server("Server-3")
            };

            foreach (var server in servers)
                loadBalancer.RegisterServer(server);

            foreach (var request in requests)
            {
                Server server = type switch
                {
                    DistributionType.RoundRobin => loadBalancer.GetServerRoundRobin(),
                    DistributionType.IPHash => loadBalancer.GetServerByIP(request),
                    DistributionType.LeastConnections => loadBalancer.GetServerLeastConnections(),
                    _ => null
                };

                server?.HandleRequest();
            }

            return servers;
        }

        static void PrintDistributionSummary(string algorithmName, List<Server> servers)
        {
            Console.WriteLine($"\n{algorithmName} Distribution:");
            foreach (var server in servers)
            {
                Console.WriteLine($"{server.Id} handled {server.RequestCount} request(s)");
            }
        }
    }
}
