using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Tests.Board
{
    public class MockBoard : Mock<Rover.Board.Board>
    {
        public MockBoard()
            : base()
        {
        }

        public void SetOffBoard()
        {
            Setup(t => t.IsOffBoard(It.IsAny<int>())).Returns(true);
        }

        public void SetCrashed()
        {
            Setup(t => t.IsOverlapping(It.IsAny<int>())).Returns(true);
        }

        public void VerifyMovedInDirection(Orientation orientation)
        {
            var (x, y)= GetDirectionChangeFromOrientation(orientation);

            Verify(t => t.MoveRelative(
                It.IsAny<int>(), 
                It.Is<int>(i => i == x), 
                It.Is<int>(i => i == y)));
        }

        private static (int, int) GetDirectionChangeFromOrientation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return (0, 1);
                case Orientation.South:
                    return (0, -1);
                case Orientation.East:
                    return (1, 0);
                case Orientation.West:
                    return (-1, 0);
                default:
                    throw new ArgumentException($"Unknown orientation: {orientation}");
            }
        }
    }
}
