using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

public class EscapeFromLabyrinth
{

    private static char[,] labyrinth;

    class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char Direction { get; private set; }
        public Point PreviousPoint { get; private set; }

        public Point(int x_ , int y_, char dir = ' ', Point prev = null)
        {
            x = x_;
            y = y_;
            Direction = dir;
            PreviousPoint = prev;
        }
    }

    private static Point FindStartPosition()
    {
        for (int row = 0; row < labyrinth.GetLength(0); row++)
        {
            for (int col = 0; col < labyrinth.GetLength(1); col++)
            {
                if ('s' == labyrinth[row, col])
                {
                    return new Point(col, row);
                }
            }
        }
        throw new InvalidOperationException("No start position selected");
    }

    private static void FindNearestExit()
    {
        var points = new Queue<Point>();
        try
        {
            points.Enqueue(FindStartPosition());

        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("No exit!");
            return;
        }
        Point point = null;
        while (points.Count > 0)
        {
            point = points.Dequeue();
            TryDirection(ref points, point, 'U');
            TryDirection(ref points, point, 'D');
            TryDirection(ref points, point, 'L');
            TryDirection(ref points, point, 'R');
        }
        var path = TraceBackwards(point);
        if (path.Equals(""))
        {
            Console.WriteLine("Start is at the exit.");
        }
        else
        {
            Console.WriteLine(path);
        }

    }

    private static string TraceBackwards(Point point)
    {
        var path = new StringBuilder();
        while (null != point)
        {
            path.Append(point.Direction);
            point = point.PreviousPoint;
        }
        return path.ToString();
    }

    private static void TryDirection(ref Queue<Point> points, Point current, char dir )
    {
        Point point = null;

        switch (dir)
        {
            case 'U':
                point = new Point(current.x, current.y-1, dir, current);
                break;
            case 'D':
                point = new Point(current.x, current.y+1, dir, current);
                break;
            case 'L':
                point = new Point(current.x-1, current.y, dir, current);
                break;
            case 'R':
                point = new Point(current.x+1, current.y, dir, current);
                break;
        }
        bool isOutside = point.x < 0 || point.y < 0 || point.x >= labyrinth.GetLength(0) ||
                         point.y >= labyrinth.GetLength(1);
        bool isAllowed = '-' == labyrinth[point.y, point.x];

        if (isOutside || !isAllowed)
        {
            return;
        }
        points.Enqueue(point);
    }

    private static void ReadLabyrinth()
    {
        int cols = int.Parse(Console.ReadLine());
        int rows = int.Parse(Console.ReadLine());

        labyrinth = new char[rows,cols];
        for (int i = 0; i < rows; i++)
        {
            var row = Console.ReadLine();
            for (var j=0; j<cols; j++)
            {
                labyrinth[i, j] = row[j];
            }
        }

    }

    public static void Main()
    {
        ReadLabyrinth();
        FindNearestExit();
    }
}
