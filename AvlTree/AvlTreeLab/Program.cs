using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvlTreeLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Range();
            Index();
        }

        static void Range()
        {
            Console.WriteLine("Add numbers separated by space");
            var nums = Console.ReadLine().Split(' ').Select(Int32.Parse).ToList();
            Console.WriteLine("Find range - two numbers separated by space.");
            var range = Console.ReadLine().Split(' ').Select(Int32.Parse).ToList();

            var tree = new AvlTree<int>();
            foreach (var num in nums)
            {
                tree.Add(num);
            }

            tree.Range(range[0], range[1]);
            Console.WriteLine();

        }

        static void Index()
        {
            var nums = new int[] { -2, 1, 0, 4, 5, 6, 10, 13, 14, 15, 17, 18, 20};
            var tree = new AvlTree<int>();
            foreach (var num in nums)
            {
                tree.Add(num);
            }

            string index;

            Console.WriteLine("Which index? 'q' for end:");
            while(true)
            {
                index = Console.ReadLine();
                if("q" == index)
                {
                    break;
                }
                try {
                    Console.WriteLine(tree[int.Parse(index)]);
                } catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
