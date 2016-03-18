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
            var nums = Console.ReadLine().Split(' ').Select(Int32.Parse).ToList();
            var range = Console.ReadLine().Split(' ').Select(Int32.Parse).ToList();

            var tree = new AvlTree<int>();
            foreach (var num in nums)
            {
                tree.Add(num);
            }

            tree.Range(range[0], range[1]);
            Console.WriteLine();
        }
    }
}
