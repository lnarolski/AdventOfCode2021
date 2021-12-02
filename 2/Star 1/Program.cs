using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_1
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
            Console.WriteLine("Hello World from Star 1!");

            var fileLines = File.ReadLines("../../../../input.txt");
            List<Command> commands = new List<Command>();

            foreach (var item in fileLines)
            {
                commands.Add(new Command(item.Split(' ')[0], int.Parse(item.Split(' ')[1])));
            }

            int[] position = { 0, 0 }; // x,y

            foreach (var item in commands)
            {
                switch (item.CommandGet())
                {
                    case "forward":
                        position[0] += item.ValueGet();
                        break;
                    case "down":
                        position[1] += item.ValueGet();
                        break;
                    case "up":
                        position[1] -= item.ValueGet();
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("position: {0},{1}  Multiplied: {2}", position[0].ToString(), position[1].ToString(), (position[0]*position[1]).ToString());
        }
    }
}
