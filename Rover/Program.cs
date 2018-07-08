using System;
using System.Collections.Generic;
using System.Linq;
using static Rover.CommandLine.Parser;

namespace Rover
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var missionControl = new MissionControl();

                var (width, height) = AskUser("Plateau", ParsePlateauArgs);

                missionControl.SetPlateauSize(width, height);

                for (int i = 1; i <= 2; i++)
                {
                    var roverName = $"Rover{i}";

                    try
                    {
                        var (x, y, o) = AskUser($"{roverName} Landing", ParseLandingArgs);

                        missionControl.Land(roverName, x, y, o);
                        
                        var instructions = AskUser($"{roverName} Instructions", ParseInstructionsArgs);
                        missionControl.Instruct(roverName, instructions);
                    }
                    catch (RoverException rex)
                    {
                        Console.WriteLine($"{roverName} has encountered a problem: {rex.Message}");
                    }
                }

                foreach (var (name, x, y, orientation) in missionControl.RoverStatus)
                {
                    Console.WriteLine($"{name}: {x} {y} {orientation.ToChar()}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        private static TReturn AskUser<TReturn>(
            string question, 
            Func<string[], TReturn> parser)
        {
            Console.Write($"{question}:");
            return parser(Console.ReadLine().Split());
        }
    }
}
