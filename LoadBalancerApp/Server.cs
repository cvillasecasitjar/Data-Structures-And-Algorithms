using System;

namespace LoadBalancerApp
{
    public class Server
    {
        // Unique identifier for the server
        public string Id { get; }

        // Tracks the number of active requests
        public int RequestCount { get; private set; }

        // Constructor
        public Server(string id)
        {
            Id = id;
            RequestCount = 0;
        }

        // Simulates handling a request
        public void HandleRequest()
        {
            RequestCount++;
            Console.WriteLine($"Server {Id} is handling request #{RequestCount}.");
        }
    }
}