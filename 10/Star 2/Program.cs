using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Star_1
{
    class Program
    {
        static private bool IsLowpoint(int x, int y, List<List<Point>> map)
        {
            if (x != 0 && map[y][x].value >= map[y][x - 1].value) // left
                return false;
            if (x != map[0].Count - 1 && map[y][x].value >= map[y][x + 1].value) //right
                return false;
            if (y != 0 && map[y][x].value >= map[y - 1][x].value) //up
                return false;
            if (y != map.Count - 1 && map[y][x].value >= map[y + 1][x].value) //down
                return false;

            return true;
        }

        enum Direction
        {
            up,
            down,
            left,
            right
        }

        class Calculator // object traveling throught basin
        {
            Direction direction;
            public long calculated = 0;
            int x;
            int y;
            public bool stopped = false;
            List<List<Point>> map;
            List<Calculator> calculatorsToAdd;

            public Calculator(int x, int y, Direction direction, List<List<Point>> map, List<Calculator> calculatorsToAdd)
            {
                this.x = x;
                this.y = y;
                this.direction = direction;
                this.map = map;
                this.calculatorsToAdd = calculatorsToAdd;
            }

            public bool Move()
            {
                switch (direction)
                {
                    case Direction.up:
                        if (y == 0 || map[y - 1][x].counted || map[y - 1][x].value == 9)
                        {
                            stopped = true;
                            return false;
                        }
                        else
                        {
                            --this.y;
                        }
                        break;
                    case Direction.down:
                        if (y == map.Count - 1 || map[y + 1][x].counted || map[y + 1][x].value == 9)
                        {
                            stopped = true;
                            return false;
                        }
                        else
                        {
                            ++this.y;
                        }
                        break;
                    case Direction.left:
                        if (x == 0 || map[y][x - 1].counted || map[y][x - 1].value == 9)
                        {
                            stopped = true;
                            return false;
                        }
                        else
                        {
                            --this.x;
                        }
                        break;
                    case Direction.right:
                        if (x == map[0].Count - 1 || map[y][x + 1].counted || map[y][x + 1].value == 9)
                        {
                            stopped = true;
                            return false;
                        }
                        else
                        {
                            ++this.x;
                        }
                        break;
                    default:
                        break;
                }

                ++calculated;
                map[y][x].counted = true;

                if (direction == Direction.up || direction == Direction.down)
                {
                    if (x != map[0].Count - 1 && !map[y][x + 1].counted && map[y][x + 1].value != 9) //right
                    {
                        calculatorsToAdd.Add(new Calculator(x, y, Direction.right, map, calculatorsToAdd));
                    }
                    if (x != 0 && !map[y][x - 1].counted && map[y][x - 1].value != 9) //left
                    {
                        calculatorsToAdd.Add(new Calculator(x, y, Direction.left, map, calculatorsToAdd));
                    }
                }
                else
                {
                    if (y != map.Count - 1 && !map[y + 1][x].counted && map[y + 1][x].value != 9) //down
                    {
                        calculatorsToAdd.Add(new Calculator(x, y, Direction.down, map, calculatorsToAdd));
                    }
                    if (y != 0 && !map[y - 1][x].counted && map[y - 1][x].value != 9) //up
                    {
                        calculatorsToAdd.Add(new Calculator(x, y, Direction.up, map, calculatorsToAdd));
                    }
                }

                return true;
            }
        }

        static private long CalculateBasinSize(int x, int y, List<List<Point>> map)
        {
            long basinSize = 1;
            List<Calculator> calculators = new List<Calculator>();
            List<Calculator> calculatorsToAdd = new List<Calculator>();
            List<int> calculatorsToDelete = new List<int>();

            if (x != 0 && !map[y][x - 1].counted && map[y][x - 1].value != 9) // left
                calculators.Add(new Calculator(x, y, Direction.left, map, calculatorsToAdd));
            if (x != map[0].Count - 1 && !map[y][x + 1].counted && map[y][x + 1].value != 9) //right
                calculators.Add(new Calculator(x, y, Direction.right, map, calculatorsToAdd));
            if (y != 0 && !map[y - 1][x].counted && map[y - 1][x].value != 9) //up
                calculators.Add(new Calculator(x, y, Direction.up, map, calculatorsToAdd));
            if (y != map.Count - 1 && !map[y + 1][x].counted && map[y + 1][x].value != 9) //down
                calculators.Add(new Calculator(x, y, Direction.down, map, calculatorsToAdd));

            while (calculators.Count > 0)
            {
                for (int i = 0; i < calculators.Count; i++)
                {
                    if (!calculators[i].Move())
                    {
                        basinSize += calculators[i].calculated;
                        calculatorsToDelete.Add(i);
                    }
                }

                for (int i = calculatorsToDelete.Count - 1; i >= 0; --i)
                {
                    calculators.RemoveAt(calculatorsToDelete[i]);
                }
                calculatorsToDelete.Clear();

                foreach (var calculator in calculatorsToAdd)
                {
                    calculators.Add(calculator);
                }
                calculatorsToAdd.Clear();
            }

            return basinSize;
        }

        class Point
        {
            public long value { set; get; }
            public bool counted { set; get; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World from Star 2!");

            var fileLines = File.ReadLines("../../../../input.txt");
            List<string> input = new List<string>();

            foreach (var item in fileLines)
            {
                input.Add(item);
            }

            // Parsing input data
            List<List<long>> temp = new List<List<long>>();
            foreach (var line in fileLines)
            {
                temp.Add(line.ToCharArray().Select(s => long.Parse(s.ToString())).ToList());
            }
            List<List<Point>> map = new List<List<Point>>();
            for (int i = 0; i < temp.Count; i++)
            {
                map.Add(new List<Point>());
                for (int j = 0; j < temp[0].Count; j++)
                {
                    map[map.Count - 1].Add(new Point() { value = temp[i][j], counted = false });
                }
            }
            //

            // Searching for an answer
            List<long> basinSizes = new List<long>();
            for (int y = 0; y < map.Count; y++)
            {
                for (int x = 0; x < map[0].Count; x++)
                {
                    if (IsLowpoint(x, y, map))
                    {
                        map[y][x].counted = true;
                        basinSizes.Add(CalculateBasinSize(x, y, map));
                    }
                }
            }
            basinSizes.Sort();
            long multiplication = basinSizes[basinSizes.Count - 1] * basinSizes[basinSizes.Count - 2] * basinSizes[basinSizes.Count - 3];
            //

            Console.WriteLine("Output: {0}", multiplication);
        }
    }
}
