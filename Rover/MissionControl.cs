using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover
{
    /// <summary>
    /// Coordinates the mission effort.  Mission control commands rovers int the
    /// field.
    /// </summary>
    public class MissionControl
    {
        public readonly List<(string name, MarsRover rover)> _rovers
            = new List<(string name, MarsRover rover)>();

        private Board.Board _plateau;

        /// <summary>
        /// Sets the size of the plateau the rovers will rove on.
        /// </summary>
        /// <param name="width">The width of the plateau.</param>
        /// <param name="height">The height of the plateau.</param>
        public void SetPlateauSize(int width, int height)
        {
            if (_plateau != null)
                throw new ArgumentException("This mission control already knows its plateau.");
            _plateau = new Board.FiniteGrid(width, height);
        }

        /// <summary>
        /// Lands a rover on the plateau.
        /// </summary>
        /// <param name="name">A name to identify the rover.</param>
        /// <param name="x">The x coordinate of the landing site.</param>
        /// <param name="y">The y coordinate of the landing site.</param>
        /// <param name="orientation">The starting orientation of the rover.</param>
        public void Land(string name, int x, int y, Orientation orientation)
        {
            if (_rovers.Any(t => t.name == name))
                throw new InvalidOperationException($"Rover {name} has already landed.");

            var rover = new MarsRover(x, y, orientation, _plateau);
            _rovers.Add((name, rover));
        }

        /// <summary>
        /// The status of all rovers controled by this MissionControl.
        /// </summary>
        public IEnumerable<(string name, int x, int y, Orientation orientation)> RoverStatus 
            => _rovers.Select(r => (
                r.name, 
                r.rover.Position.x, 
                r.rover.Position.y, 
                r.rover.Orientation));

        /// <summary>
        /// Instructs a rover to carry out a series of commands.
        /// </summary>
        /// <param name="name">The name of the rover.</param>
        /// <param name="commands">A list of commands for the rover to carry out.</param>
        public void Instruct(string name, IEnumerable<RoverCommands> commands)
        {
            var rover = _rovers.Single(kvp => kvp.name == name).rover;

            foreach (var command in commands)
            {
                switch (command)
                {
                    case RoverCommands.Left:
                        rover.TurnLeft();
                        break;
                    case RoverCommands.Right:
                        rover.TurnRight();
                        break;
                    case RoverCommands.Move:
                        rover.Move();
                        break;
                }
            };
        }
    }

    public enum RoverCommands
    {
        Left,
        Right,
        Move
    }
}
