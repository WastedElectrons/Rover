using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.CommandLine
{
    public static class Parser
    {
        public static (int, int) ParsePlateauArgs(string[] input)
        {
            if (input.Length != 2)
                throw new ArgumentException("Incorrect plateau dimensions.");

            var dimlist = input.Select(s => ParseInt(s)).ToList();
            return (dimlist[0], dimlist[1]);
        }

        public static (int, int, Orientation) ParseLandingArgs(string[] input)
        {
            if (input.Length != 3)
                throw new ArgumentException("Incorrect landing format.");

            return (
                ParseInt(input[0]),
                ParseInt(input[1]),
                OrientationFromChar(input[2]));
        }

        public static IEnumerable<RoverCommands> ParseInstructionsArgs(string[] input)
        {
            if (input.Length != 1)
                throw new ArgumentException("Incorrect instruction format.");

            return input[0].Select(CommandFromChar);
        }

        private static int ParseInt(string s)
        {
            if (!int.TryParse(s, out int ret))
            {
                throw new ArgumentException($"{s} is not a number");
            }
            return ret;
        }

        public static RoverCommands CommandFromChar(char c)
        {
            switch (c)
            {
                case 'M':
                case 'm':
                    return RoverCommands.Move;
                case 'L':
                case 'l':
                    return RoverCommands.Left;
                case 'R':
                case 'r':
                    return RoverCommands.Right;
            }

            throw new ArgumentException($"Missing command: {c}");
        }

        public static Orientation OrientationFromChar(string s)
        {
            if (s.Length != 1)
                throw new ArgumentException("Incorrect orientation format.");

            return OrientationFromChar(s[0]);
        }

        public static Orientation OrientationFromChar(char c)
        {
            switch (c)
            {
                case 'N':
                case 'n':
                    return Orientation.North;
                case 'E':
                case 'e':
                    return Orientation.East;
                case 'S':
                case 's':
                    return Orientation.South;
                case 'W':
                case 'w':
                    return Orientation.West;
            }

            throw new ArgumentException($"Missing orientation: {c}");
        }
    }
}
