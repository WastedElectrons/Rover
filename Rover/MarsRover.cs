using System;
using System.Collections.Generic;
using System.Text;

namespace Rover
{
    /// <summary>
    /// A Rover that can be driven on the plateau.
    /// 
    /// Rovers obay the following commands:
    /// 
    /// Turn Left
    /// Turn Right
    /// Move Forward
    /// </summary>
    public class MarsRover
    {
        private readonly Board.Board _board;
        private readonly int _boardPeice;

        /// <summary>
        /// Constructor used when landing the rover.
        /// </summary>
        /// <param name="x">The x coordinate of the landing site.</param>
        /// <param name="y">The y coordinate of the landing site.</param>
        /// <param name="orientation">The rovers starting orientation.</param>
        /// <param name="board">The plateau the rover will land on.</param>
        public MarsRover(int x, int y, Orientation orientation, Board.Board board)
        {
            _board = board;
            _boardPeice = _board.Place(x, y);
            Orientation = orientation;

            AssertDrivable();
        }

        /// <summary>
        /// The current orientation of the rover.
        /// </summary>
        public Orientation Orientation { get; private set; }

        /// <summary>
        /// The current position of the rover.
        /// </summary>
        public (int x, int y) Position => _board.GetPosition(_boardPeice);

        /// <summary>
        /// Turns the rover 90 degrees to the left.
        /// </summary>
        public void TurnLeft()
        {
            AssertDrivable();
            switch (Orientation)
            {
                case (Orientation.North):
                    Orientation = Orientation.West;
                    break;
                case (Orientation.West):
                    Orientation = Orientation.South;
                    break;
                case (Orientation.South):
                    Orientation = Orientation.East;
                    break;
                case (Orientation.East):
                    Orientation = Orientation.North;
                    break;
            }
        }


        /// <summary>
        /// Turns the rover 90 degrees to the right.
        /// </summary>
        public void TurnRight()
        {
            AssertDrivable();
            switch (Orientation)
            {
                case (Orientation.North):
                    Orientation = Orientation.East;
                    break;
                case (Orientation.East):
                    Orientation = Orientation.South;
                    break;
                case (Orientation.South):
                    Orientation = Orientation.West;
                    break;
                case (Orientation.West):
                    Orientation = Orientation.North;
                    break;
            }
        }

        /// <summary>
        /// Moves the rover forward one unit.
        /// </summary>
        public void Move()
        {
            AssertDrivable();
            switch (Orientation)
            {
                case (Orientation.North):
                    _board.MoveRelative(_boardPeice, 0, 1);
                    break;
                case (Orientation.East):
                    _board.MoveRelative(_boardPeice, 1, 0);
                    break;
                case (Orientation.South):
                    _board.MoveRelative(_boardPeice, 0, -1);
                    break;
                case (Orientation.West):
                    _board.MoveRelative(_boardPeice, -1, 0);
                    break;
            }
        }

        private void AssertDrivable()
        {
            if (_board.IsOffBoard(_boardPeice))
                throw new RoverException("The rover has driven off the plateau");

            if (_board.IsOverlapping(_boardPeice))
                throw new RoverException("The rover has crashed into another rover");
                
        }
    }
}
