using System;

class Program
{
    static void Main(string[] args)
    {
        Graph graph = new Graph();

        // Adding at least 6 nodes and edges
        graph.AddEdge("A", "B");
        graph.AddEdge("A", "C");
        graph.AddEdge("B", "D");
        graph.AddEdge("B", "E");
        graph.AddEdge("C", "F");
        graph.AddEdge("E", "F");

        graph.PrintGraph();

        // Perform DFS and BFS
        graph.DFS("A");
        graph.BFS("A");
    }
}
