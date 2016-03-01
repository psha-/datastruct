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

    public static void Main()
    {
        try {
            var count = int.Parse(Console.ReadLine());
            graph = new List<int>[count];
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
            FindConnectedComponent();

        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.ReadLine();
    }
}
