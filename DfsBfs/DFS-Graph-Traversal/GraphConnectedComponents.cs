using System;
using System.Collections.Generic;

public class GraphConnectedComponents
{
    private static bool[] visited;
    private static List<int>[] graph;

    static void DFS(int node)
    {
        if (visited[node])
        {
            return;
        }
        visited[node] = true;
        foreach (var child in graph[node])
        {
            DFS(child);
        }
        Console.Write(" {0}", node);
    }

    static void FindConnectedComponent()
    {

        visited = new bool[graph.Length];
        for (int i = 0; i < graph.Length; i++)
        {
            if (visited[i])
            {
                continue;
            }
            Console.Write("Connected component:");
            DFS(i);
            Console.WriteLine();
        }

    }

    private static List<int>[] ReadGraph()
    {
        var count = int.Parse(Console.ReadLine());
        var graph = new List<int>[count];
        for (int i = 0; i < count; i++)
        {
            string[] vals = Console.ReadLine().Split();
            graph[i] = new List<int>(vals.Length);
            foreach (var val in vals)
            {
                int v;
                if (int.TryParse(val, out v))
                {
                    graph[i].Add(v);
                }
            }
        }
        return graph;
    }

    public static void Main()
    {
        graph = ReadGraph();
        FindConnectedComponent();
        Console.ReadLine();
    }
}
