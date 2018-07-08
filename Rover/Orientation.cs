using System;
using System.Collections.Generic;
using System.Text;

namespace Rover
{
    public enum Orientation
    {
        North,
        South,
        East,
        West,
    }

    public static class OrientationExtensions
    {
        public static char ToChar(this Orientation o)
        {
            switch (o)
            {
                case Orientation.North:
                    return 'N';
                case Orientation.East:
                    return 'E';
                case Orientation.South:
                    return 'S';
                case Orientation.West:
                    return 'W';
            }

            throw new ArgumentException($"Missing orientation: {o}");
        }
    }
}
