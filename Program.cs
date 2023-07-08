using System;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            ToyRobot robot = new ToyRobot();

            // Test data
            string[] commands = {
            "PLACE 0,0,NORTH",
            "MOVE",
            "REPORT",
            "PLACE 0,0,NORTH",
            "LEFT",
            "REPORT",
            "PLACE 1,2,EAST",
            "MOVE",
            "MOVE",
            "LEFT",
            "MOVE",
            "REPORT"
        };

            ProcessCommands(robot, commands);
        }

        private static void ProcessCommands(ToyRobot robot, string[] commands)
        {
            foreach (var command in commands)
            {
                string[] commandParts = command.Split(' ');
                string commandType = commandParts[0];

                if (commandType == "PLACE" && commandParts.Length == 2)
                {
                    string[] position = commandParts[1].Split(',');
                    int x = int.Parse(position[0]);
                    int y = int.Parse(position[1]);
                    string facing = position[2];
                    robot.Place(x, y, facing);
                }
                else if (robot.isPlaced)
                {
                    switch (commandType)
                    {
                        case "MOVE":
                            robot.Move();
                            break;
                        case "LEFT":
                            robot.Left();
                            break;
                        case "RIGHT":
                            robot.Right();
                            break;
                        case "REPORT":
                            robot.Report();
                            break;
                    }
                }
            }
        }
    }

    public class ToyRobot
    {
        private int x;
        private int y;
        private string facing;
        public bool isPlaced;

        public ToyRobot()
        {
            x = 0;
            y = 0;
            facing = "";
            isPlaced = false;
        }

        public void Place(int x, int y, string facing)
        {
            if (IsValidPosition(x, y) && IsValidDirection(facing))
            {
                this.x = x;
                this.y = y;
                this.facing = facing;
                isPlaced = true;
            }
        }

        public void Move()
        {
            if (!isPlaced)
                return;

            var (newX, newY) = GetNewPosition();

            if (IsValidPosition(newX, newY))
            {
                x = newX;
                y = newY;
            }
        }

        public void Left()
        {
            if (isPlaced)
            {
                string[] directions = { "NORTH", "WEST", "SOUTH", "EAST" };
                int currentIndex = Array.IndexOf(directions, facing);
                facing = directions[(currentIndex + 3) % 4];
            }
        }

        public void Right()
        {
            if (isPlaced)
            {
                string[] directions = { "NORTH", "EAST", "SOUTH", "WEST" };
                int currentIndex = Array.IndexOf(directions, facing);
                facing = directions[(currentIndex + 1) % 4];
            }
        }

        public void Report()
        {
            if (isPlaced)
                Console.WriteLine($"Current position: {x},{y},{facing}");
            else
                Console.WriteLine("Robot is not placed on the table.");
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x <= 5 && y >= 0 && y <= 5;
        }

        private bool IsValidDirection(string facing)
        {
            string[] validDirections = { "NORTH", "SOUTH", "EAST", "WEST" };
            return Array.IndexOf(validDirections, facing) != -1;
        }

        private (int, int) GetNewPosition()
        {
            int newX = x;
            int newY = y;

            if (facing == "NORTH")
                newY++;
            else if (facing == "SOUTH")
                newY--;
            else if (facing == "EAST")
                newX++;
            else if (facing == "WEST")
                newX--;

            return (newX, newY);
        }
    }
}
