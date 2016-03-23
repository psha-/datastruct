using System;

namespace QuadTree
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Available commands: add, move, start, tick, pause, end.");
            var game = new Game();
            string command;

            do
            {
                var input = Console.ReadLine();
                command = ParseCommand(input);

                switch (command)
                {
                    case "add":
                        var arguments = ParseArguments(input);
                        try
                        {
                            game.AddObject(arguments[0], int.Parse(arguments[1]), int.Parse(arguments[2]));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Not enough arguments.");
                        }
                        break;
                    case "start":
                        game.Start();
                        break;
                }

                while (game.Running)
                {
                    input = Console.ReadLine();
                    command = ParseCommand(input);
                    switch (command)
                    {
                        case "end":
                        case "pause":
                            game.End();
                            break;
                        case "tick":
                            game.PrintCollisions();
                            break;
                        case "move":
                            var arguments = ParseArguments(input);
                            try
                            {
                                game.MoveObject(arguments[0], int.Parse(arguments[1]), int.Parse(arguments[2]));
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Not enough arguments.");
                            }
                            game.PrintCollisions();
                            break;
                    }
                    game.Tick();
                }

            } while ("end" != command);
        }

        public static string ParseCommand(string input)
        {
            if (-1 == input.IndexOf(' '))
            {
                return input;
            }
            return input.Substring(0, input.IndexOf(' ')).ToLower();

        }
        public static string[] ParseArguments(string input)
        {
            return input.Substring(input.IndexOf(' ') + 1).Split(' ');
        }
    }
}
