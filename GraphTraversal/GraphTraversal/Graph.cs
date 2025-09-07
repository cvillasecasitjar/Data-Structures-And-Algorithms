using System;
using System.Collections.Generic;

public class Graph
{
    private Dictionary<string, List<string>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<string, List<string>>();
    }

    public void AddEdge(string vertex, string neighbor)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            adjacencyList[vertex] = new List<string>();
        }
        adjacencyList[vertex].Add(neighbor);

        // Optional: Add the reverse edge for undirected graph
        /*
        if (!adjacencyList.ContainsKey(neighbor))
        {
            adjacencyList[neighbor] = new List<string>();
        }
        adjacencyList[neighbor].Add(vertex);
        */
    }

    public void PrintGraph()
    {
        Console.WriteLine("Graph Adjacency List:");
        foreach (var vertex in adjacencyList)
        {
            Console.Write(vertex.Key + " → ");
            Console.WriteLine(string.Join(", ", vertex.Value));
        }
    }

    public void DFS(string start)
    {
        var visited = new HashSet<string>();
        Console.WriteLine("\nDFS Traversal starting from " + start + ":");
        DFSRecursive(start, visited);
        Console.WriteLine();
    }

    private void DFSRecursive(string vertex, HashSet<string> visited)
    {
        if (!adjacencyList.ContainsKey(vertex) || visited.Contains(vertex))
            return;

        Console.Write(vertex + " ");
        visited.Add(vertex);

        foreach (var neighbor in adjacencyList[vertex])
        {
            DFSRecursive(neighbor, visited);
        }
    }

    public void BFS(string start)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<string>();

        Console.WriteLine("\nBFS Traversal starting from " + start + ":");

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Console.Write(current + " ");

            if (!adjacencyList.ContainsKey(current))
                continue;

            foreach (var neighbor in adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        Console.WriteLine();
    }
}
