using System;
using System.Collections.Generic;
using System.Linq;

namespace LoadBalancerApp
{
    public class LoadBalancer
    {
        private List<Server> servers = new List<Server>();
        private int roundRobinIndex = 0;

        // Register a new server
        public void RegisterServer(Server server)
        {
            servers.Add(server);
            Console.WriteLine($"Registered server: {server.Id}");
        }

        // Round-Robin Strategy
        public Server GetServerRoundRobin()
        {
            if (servers.Count == 0) return null;

            var server = servers[roundRobinIndex];
            roundRobinIndex = (roundRobinIndex + 1) % servers.Count;
            return server;
        }

        // IP Hashing Strategy
        public Server GetServerByIP(string ipAddress)
        {
            if (servers.Count == 0) return null;

            int hash = ipAddress.GetHashCode();
            int index = Math.Abs(hash % servers.Count);
            return servers[index];
        }

        // Least Connections Strategy
        public Server GetServerLeastConnections()
        {
            if (servers.Count == 0) return null;

            return servers.OrderBy(s => s.RequestCount).First();
        }
    }
}