using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Star_2
{
    class Command
    {
        private string command { get; set; }
        private int value { get; set; }

        public string CommandGet() { return this.command; }
        public int ValueGet() { return this.value; }

        public Command(string command, int value)
        {
            this.command = command;
            this.value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from Star 2!");

            var fileLines = File.ReadLines("../../../../input.txt");
            List<Command> commands = new List<Command>();

            foreach (var item in fileLines)
            {
                commands.Add(new Command(item.Split(' ')[0], int.Parse(item.Split(' ')[1])));
            }

            int[] position = { 0, 0, 0 }; // x,y,aim

            foreach (var item in commands)
            {
                switch (item.CommandGet())
                {
                    case "forward":
                        position[0] += item.ValueGet();
                        position[1] += position[2] * item.ValueGet();
                        break;
                    case "down":
                        position[2] += item.ValueGet();
                        break;
                    case "up":
                        position[2] -= item.ValueGet();
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("position: {0},{1} aim:{3} Multiplied: {2}", position[0].ToString(), position[1].ToString(), (position[0] * position[1]).ToString(), position[2]);
        }
    }
}
